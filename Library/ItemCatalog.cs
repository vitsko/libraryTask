namespace Library
{
    using System;

    public abstract class ItemCatalog //: IComparable<ItemCatalog>
    {
        protected const char Charp = '#';

        public string Title { get; set; }

        public int PageCount { get; set; }

        public string Note { get; set; }

        public int Id { get; protected set; }

        // internal abstract int PublishedYear { get; }

        //public int CompareTo(ItemCatalog other)
        //{
        //    if (this.PublishedYear > other.PublishedYear)
        //    {
        //        return 1;
        //    }

        //    if (this.PublishedYear < other.PublishedYear)
        //    {
        //        return -1;
        //    }

        //    return 0;
        //}

        protected static int GetId()
        {
            return Catalog.Count == 0 ? 1 : Catalog.Count + 1;
        }

        protected abstract void Create(string[] aboutItemCatalog);
    }
}
