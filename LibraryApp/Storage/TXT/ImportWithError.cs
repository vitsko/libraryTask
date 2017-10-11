namespace LibraryApp.Storage.TXT
{
    using System.Collections.Generic;
    using System.IO;
    using Helper;
    using Library;
    using Resource;

    internal class ImportWithError : Import
    {
        public override bool ImportToFile(string fileName)
        {
            var reader = new StreamReader(fileName);
            var stringsFromFile = new List<List<string>>();

            while (reader.Peek() >= 0)
            {
                var aboutItem = reader.ReadLine();

                List<string> afterParsing;

                if (Helper.IsTypeOfItemCatalog(aboutItem, out afterParsing))
                {
                    stringsFromFile.Add(Helper.ParseStringToItem(afterParsing));
                }
                else
                {
                    Screen.WriteLog(Titles.AboutIncorrectTypeItem);
                    Screen.WriteLog(aboutItem);

                    continue;
                }
            }

            reader.Close();

            if (stringsFromFile.Count != 0)
            {
                Catalog.Load(stringsFromFile);
                Screen.WriteLog(Screen.ResultImportToLog());

                return true;
            }

            return false;
        }
    }
}