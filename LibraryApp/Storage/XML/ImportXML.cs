namespace LibraryApp.Storage.XML
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    using Helper;
    using Library;
    using Resource;

    internal class ImportXML : Import
    {
        public override bool ImportToFile(string fileName)
        {
            XmlReader xmlReader = null;

            try
            {
                var serializer = new XmlSerializer(typeof(List<ItemCatalog>));

                xmlReader = XmlReader.Create(fileName);
                Helper.XmlRead = xmlReader;

                var catalog = (List<ItemCatalog>)serializer.Deserialize(Helper.XmlRead);

                Catalog.RewriteCatalog(catalog);

                Screen.WriteLog(string.Format(Titles.ToLogCorrectImport, Environment.CurrentDirectory, fileName));
            }
            catch (Exception exception)
            {
                var log = Screen.AboutError(exception);
                Screen.WriteLog(log);
                return false;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                    Helper.XmlRead = null;
                }
            }

            return true;
        }
    }
}