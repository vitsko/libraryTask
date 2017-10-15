namespace LibraryApp.Storage.TXT
{
    using System.Collections.Generic;
    using System.IO;
    using Helper;
    using Library;

    internal class ImportNotError : Import
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
                    return false;
                }
            }

            reader.Close();

            return Catalog.LoadWithoutError(stringsFromFile);
        }
    }
}