namespace LibraryApp.Storage.TXT
{
    internal class FactoryTXTWithError : StorageFactory
    {
        public override Export CreateExport()
        {
            return new ExportTxt();
        }

        public override Import CreateImport()
        {
            return new ImportWithError();
        }
    }
}