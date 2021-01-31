namespace FontMapper
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoadTranslationFile = new System.Windows.Forms.Button();
            this.openTranslationDoc = new System.Windows.Forms.OpenFileDialog();
            this.lblLoadedTranslationFile = new System.Windows.Forms.Label();
            this.rtbSource = new System.Windows.Forms.RichTextBox();
            this.rtbDestination = new System.Windows.Forms.RichTextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cmbSourceLanguages = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTargetLanguages = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnLoadTranslationFile
            // 
            this.btnLoadTranslationFile.Location = new System.Drawing.Point(21, 12);
            this.btnLoadTranslationFile.Name = "btnLoadTranslationFile";
            this.btnLoadTranslationFile.Size = new System.Drawing.Size(168, 23);
            this.btnLoadTranslationFile.TabIndex = 0;
            this.btnLoadTranslationFile.Text = "Load Translation File";
            this.btnLoadTranslationFile.UseVisualStyleBackColor = true;
            this.btnLoadTranslationFile.Click += new System.EventHandler(this.btnLoadTranslationFile_Click);
            // 
            // openTranslationDoc
            // 
            this.openTranslationDoc.FileName = global::FontMapper.Properties.Settings.Default.CopticFontsPath;
            this.openTranslationDoc.Filter = "Word Files|*.docx";
            // 
            // lblLoadedTranslationFile
            // 
            this.lblLoadedTranslationFile.AutoSize = true;
            this.lblLoadedTranslationFile.Location = new System.Drawing.Point(195, 17);
            this.lblLoadedTranslationFile.Name = "lblLoadedTranslationFile";
            this.lblLoadedTranslationFile.Size = new System.Drawing.Size(117, 13);
            this.lblLoadedTranslationFile.TabIndex = 1;
            this.lblLoadedTranslationFile.Text = "(No files were selected)";
            // 
            // rtbSource
            // 
            this.rtbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbSource.Location = new System.Drawing.Point(12, 97);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.Size = new System.Drawing.Size(425, 236);
            this.rtbSource.TabIndex = 2;
            this.rtbSource.Text = "";
            // 
            // rtbDestination
            // 
            this.rtbDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDestination.Location = new System.Drawing.Point(524, 97);
            this.rtbDestination.Name = "rtbDestination";
            this.rtbDestination.Size = new System.Drawing.Size(423, 236);
            this.rtbDestination.TabIndex = 3;
            this.rtbDestination.Text = "";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(443, 187);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 53);
            this.btnConvert.TabIndex = 4;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // cmbSourceLanguages
            // 
            this.cmbSourceLanguages.FormattingEnabled = true;
            this.cmbSourceLanguages.Location = new System.Drawing.Point(94, 60);
            this.cmbSourceLanguages.Name = "cmbSourceLanguages";
            this.cmbSourceLanguages.Size = new System.Drawing.Size(293, 21);
            this.cmbSourceLanguages.TabIndex = 5;
            this.cmbSourceLanguages.SelectedIndexChanged += new System.EventHandler(this.cmbSourceLanguages_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Source Font";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(521, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Destination Font";
            // 
            // cmbTargetLanguages
            // 
            this.cmbTargetLanguages.FormattingEnabled = true;
            this.cmbTargetLanguages.Location = new System.Drawing.Point(613, 58);
            this.cmbTargetLanguages.Name = "cmbTargetLanguages";
            this.cmbTargetLanguages.Size = new System.Drawing.Size(279, 21);
            this.cmbTargetLanguages.TabIndex = 8;
            this.cmbTargetLanguages.SelectedIndexChanged += new System.EventHandler(this.cmbTargetLanguages_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 345);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTargetLanguages);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSourceLanguages);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.rtbDestination);
            this.Controls.Add(this.rtbSource);
            this.Controls.Add(this.lblLoadedTranslationFile);
            this.Controls.Add(this.btnLoadTranslationFile);
            this.Name = "frmMain";
            this.Text = "Font Mapper";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadTranslationFile;
        private System.Windows.Forms.OpenFileDialog openTranslationDoc;
        private System.Windows.Forms.Label lblLoadedTranslationFile;
        private System.Windows.Forms.RichTextBox rtbSource;
        private System.Windows.Forms.RichTextBox rtbDestination;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ComboBox cmbSourceLanguages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTargetLanguages;
    }
}

