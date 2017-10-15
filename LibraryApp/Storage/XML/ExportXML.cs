namespace LibraryApp.Storage.XML
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using Library;

    internal class ExportXML : Export
    {
        public override bool ExportToFile(string fileName)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<ItemCatalog>));
                StreamWriter writer = new StreamWriter(fileName);
                serializer.Serialize(writer, Catalog.AllItem);
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