using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace FontMapper
{
    public partial class frmMain : Form
    {


        Dictionary<string, Dictionary<String, Dictionary<String, String>>> dictionaries;
        List<string> NON_TOKENIZED = new List<string>() { " ", "-", "_", "—", "\n", "\r", "\t" };
        const String FONT_NAME_PATTERN = @"([\w_ ]*).ttf";



        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLoadTranslationFile_Click(object sender, EventArgs e)
        {
            if (openTranslationDoc.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    LoadFontsFile();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Can't Open the file , are you sure the file is closed ?");
                }
                finally
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void LoadFontsFile()
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(openTranslationDoc.FileName, false))
            {

                try
                {
                    Table table = doc.MainDocumentPart.Document.Body.Elements<Table>().First();

                    TableRow headerRow = table.ChildElements.First<TableRow>();

                    var fontNames = headerRow.Elements<TableCell>().Select(x => x.InnerText);

                    string[] errorMessages;

                    if (!CheckInstalledFonts(fontNames, out errorMessages))
                    {
                        MessageBox.Show(String.Join("\r\n", errorMessages), "Fonts error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    var rows = table.ChildElements.OfType<TableRow>().Skip(1);
                    dictionaries = new Dictionary<string, Dictionary<String, Dictionary<String, String>>>();



                    foreach (var font in fontNames)
                    {
                        dictionaries.Add(font, new Dictionary<String, Dictionary<String, String>>());
                        foreach (var key in fontNames)
                        {
                            if (key != font)
                            {
                                dictionaries[font].Add(key, new Dictionary<string, string>());
                            }
                        }
                    }


                    for (int i = 0; i < rows.Count(); i++)
                    {
                        TableRow row = rows.ElementAt(i);
                        var cells = row.Elements<TableCell>();
                        for (int li = 0; li < cells.Count(); li++)
                        {
                            var cellText = cells.ElementAt(li).InnerText;

                            for (int oci = 0; oci < cells.Count(); oci++)
                            {
                                var otherCellText = cells.ElementAt(oci).InnerText;

                                if (li != oci)
                                {
                                    dictionaries.ElementAt(li).Value[dictionaries.Keys.ElementAt(oci)][cellText] = otherCellText;
                                }
                            }
                        }
                    }

                    // populate the combos
                    cmbTargetLanguages.DataSource = fontNames.ToList();
                    cmbSourceLanguages.DataSource = fontNames.ToList();
                    lblLoadedTranslationFile.Text = openTranslationDoc.FileName;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }


        /// <summary>
        /// Checks the if fonts are installed on the computer. Whenever a font is not found, it automatically checks the font folder and prompts an install if found.
        /// 
        /// </summary>
        /// <param name="fileFonts"></param>
        /// <returns>The error message</returns>
        private static bool CheckInstalledFonts(IEnumerable<string> fileFonts, out String[] errorMessages)
        {
            var errList = new List<String>();
            InstalledFontCollection coll = new InstalledFontCollection();

            foreach (var fontFamilyName in fileFonts)
            {
                var searchedFont = coll.Families.FirstOrDefault(f => f.Name.ToLower() == fontFamilyName.ToLower());
                if (searchedFont == null)
                {
                    var builtinFonts = Directory.GetFiles("Fonts", "*.ttf").ToList();

                    var fetchedFont = builtinFonts.FirstOrDefault(x => Regex.Match(x, FONT_NAME_PATTERN).Groups[1].Value.ToLower() == fontFamilyName.ToLower());



                    String msg;
                    if (fetchedFont != null)
                    {

                        var fetchedFontName = Regex.Match(fetchedFont, FONT_NAME_PATTERN).Groups[1].Value;


                        msg = String.Format("Font: {0} is not installed on your computer, but it's one of the built-in fonts , install ?", fontFamilyName);
                        if (DialogResult.Yes == MessageBox.Show(msg, "Font not found", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            // install font
                            var cmd = String.Format(
                            @"XCOPY ""{0}"" ""{1}\"" /Y", ///Y && reg add ""HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts"" /v ""{2}"" /t REG_SZ /d ""{3}"" /f", 
                            fetchedFont,
                            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts),
                            fetchedFontName,
                            fetchedFontName + Path.GetExtension(fetchedFont)

                            );
                            Process p = new Process();
                            ProcessStartInfo info = new ProcessStartInfo(cmd);
                            info.UseShellExecute = false;
                            info.RedirectStandardOutput = true;

                            p.StartInfo = info;
                            p.Start();

                            string output = p.StandardOutput.ReadToEnd();
                            MessageBox.Show(output);
                            p.WaitForExit();
                        }

                    }
                    else
                    {
                        errList.Add(String.Format("Font: {0} is not installed on your computer please download and install", fontFamilyName));

                    }


                }
            }

            errorMessages = errList.ToArray();

            return errList.Count == 0;

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            String sourceLang = cmbSourceLanguages.SelectedValue.ToString();
            String destLang = cmbTargetLanguages.SelectedValue.ToString();

            if (sourceLang == null || destLang == null || sourceLang == destLang)
            {
                MessageBox.Show("Different Languages must be selected");
                return;
            }
            if (dictionaries == null)
            {
                MessageBox.Show("Dictionary must be loaded");
                return;
            }

            var sourceLangTokens = dictionaries[sourceLang][destLang].Keys.ToArray();
            // tokenize
            List<string> tokenizedSource = Tokenize(rtbSource.Text, sourceLangTokens);

            List<string> translatedTokens;
            try
            {
                //translate 
                translatedTokens = TranslateTokens(tokenizedSource, dictionaries[sourceLang][destLang]);

                //translate
                rtbDestination.Text = string.Join(String.Empty, translatedTokens);

                rtbDestination.SelectAll();

                rtbDestination.SelectionFont = new System.Drawing.Font(destLang, rtbSource.Font.Size);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private List<string> TranslateTokens(List<string> tokenizedSource, Dictionary<string, string> dictionary)
        {
            var result = new List<String>();
            foreach (var token in tokenizedSource)
            {
                if (NON_TOKENIZED.Contains(token))
                {
                    result.Add(token);
                }
                else
                {
                    result.Add(dictionary[token]);
                }
            }

            return result;
        }

        private List<string> Tokenize(string src, string[] sourceTokens)
        {

            var sourceLangTokens = sourceTokens.OrderByDescending(x => x.Length).ToList();
            string token = string.Empty;
            bool aggregateToken = false;

            var allTokens = new List<string>();

            for (int i = 0; i < src.Length; i++)
            {
                if (aggregateToken)
                {
                    token += src[i].ToString();
                }
                else
                {
                    token = src[i].ToString();
                }
                if (sourceLangTokens.Contains(token) || NON_TOKENIZED.Contains(token))
                {
                    allTokens.Add(token);
                    aggregateToken = false;
                }
                else
                {
                    aggregateToken = true;
                }
            }

            return allTokens;
        }

        private void cmbSourceLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbSource.SelectAll();
            rtbSource.SelectionFont = new System.Drawing.Font(cmbSourceLanguages.SelectedValue.ToString(), 16f);
        }

        private void cmbTargetLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {

            rtbDestination.SelectAll();
            rtbDestination.SelectionFont = new System.Drawing.Font(cmbTargetLanguages.SelectedValue.ToString(), 16f);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var fontsPathSettings = openTranslationDoc.FileName;
            if (!string.IsNullOrWhiteSpace(fontsPathSettings))
            {
                lblLoadedTranslationFile.Text = openTranslationDoc.FileName;
                LoadFontsFile();
            }
            else
            {
                openTranslationDoc.InitialDirectory = Directory.GetCurrentDirectory();
            }
        }
    }
}
