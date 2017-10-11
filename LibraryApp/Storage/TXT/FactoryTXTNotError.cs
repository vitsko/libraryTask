namespace LibraryApp.Storage.TXT
{
    internal class FactoryTXTNotError : StorageFactory
    {
        public override Export CreateExport()
        {
            return new ExportTxt();
        }

        public override Import CreateImport()
        {
            return new ImportNotError();
        }
    }
}
