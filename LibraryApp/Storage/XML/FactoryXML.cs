namespace LibraryApp.Storage.XML
{
    internal class FactoryXML : StorageFactory
    {
        public override Export CreateExport()
        {
            return new ExportXML();
        }

        public override Import CreateImport()
        {
            return new ImportXML();
        }
    }
}
