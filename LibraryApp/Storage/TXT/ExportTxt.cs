namespace LibraryApp.Storage.TXT
{
    using System.IO;
    using Library;

    internal class ExportTxt : Export
    {
        public override bool ExportToFile(string fileName)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileName);

                writer.Write(Catalog.Save());
                writer.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}