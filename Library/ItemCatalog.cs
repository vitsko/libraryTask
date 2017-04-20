namespace Library
{
    using System;

    public abstract class ItemCatalog : IComparable<ItemCatalog>
    {
        public string Title { get; set; }

        public string PageCount { get; set; }

        public string Note { get; set; }

        internal abstract int PublishedYear { get; }

        public int CompareTo(ItemCatalog other)
        {
            if (this.PublishedYear > other.PublishedYear)
            {
                return 1;
            }

            if (this.PublishedYear < other.PublishedYear)
            {
                return -1;
            }

            return 0;
        }

        protected abstract void Create(string[] aboutItemCatalog);
    }
}
