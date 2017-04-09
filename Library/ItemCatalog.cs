namespace Library
{
    public abstract class ItemCatalog
    {
        public string Title { get; set; }

        public string PageCount { get; set; }

        public string Note { get; set; }

        protected abstract void Create(string[] aboutItemCatalog);
    }
}
