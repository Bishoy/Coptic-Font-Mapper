## Coptic Font Mapper

I've worked on this project years ago when we needed to map Coptic fonts to one another.
At the time, This was an issue with non-unicode coptic fonts which is better resolved currently with using unicode fonts. 
However, it can be useful for already current text so i thought to put it out there in case somebody still needs it. 
I also believe it should work for other non-unicode fonts, if the correct map file is provided of course.

### How It Works
1. A file Coptic-Fonts-File.docx should be created and will contain a table with which characters/tokens map to which other characters in the target font
A sample file is already included in the source above 
2. The source and target fonts are selected from the drop downs
3. The two text boxes one for source and the other for target can be used to convert the font.

### Known Issues
The application will attempt to install the fonts it has (in fonts folder) , however that doesn't necessarily work :D .. 
For that, Please install the fonts you need to convert beforehand.


### Download and use
The latest release will be found in the releases section of the project

### Further development to this project
If you need modifications to this projects maybe open an issue and i will look at it at the earliest convenience , Forks/PRs are welcome as well. 