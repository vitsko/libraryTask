namespace LibraryApp.Storage
{
    internal class Storage
    {
        private Export export;
        private Import import;

        private string fileName;

        internal Storage(string fileName, StorageFactory factory)
        {
            this.export = factory.CreateExport();
            this.import = factory.CreateImport();
            this.fileName = fileName;
        }

        internal string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        internal bool Export()
        {
            return this.export.ExportToFile(this.fileName);
        }

        internal bool Import()
        {
            return this.import.ImportToFile(this.fileName);
        }
    }
}
