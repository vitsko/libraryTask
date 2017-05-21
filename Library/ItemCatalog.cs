namespace Library
{
    using Resource;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class ItemCatalog : IComparable<ItemCatalog>
    {
        protected const char Comma = ',';
        protected const char Charp = '#';
        protected const int DefaultPageCount = 100;
        protected static List<string> errorList = new List<string>();

        private string title;
        private int pageCount;

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.title = value;
                }
                else
                {
                    this.title = Titles.DefaultTitle;
                    ItemCatalog.errorList.Add(string.Format(Titles.TitleError, this.title));
                }
            }
        }

        public int PageCount
        {
            get
            {
                return this.pageCount;
            }
            set
            {
                if (value != 0)
                {
                    this.pageCount = value;
                }
                else
                {
                    this.pageCount = ItemCatalog.DefaultPageCount;
                    ItemCatalog.errorList.Add(string.Format(Titles.PageCountError, this.pageCount));
                }
            }
        }

        public string Note { get; set; }

        public int Id { get; protected set; }

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

        public static bool IsCorrectCreating()
        {
            return ItemCatalog.errorList.Count == 0;
        }

        public static List<string> GetListOfError()
        {
            return ItemCatalog.errorList;
        }

        protected static int GetId()
        {
            return Catalog.Count == 0 ? 1 : Catalog.AllItem.ElementAt(Catalog.Count - 1).Id + 1;
        }

        protected internal abstract void Create(List<string> aboutItemCatalog);

        public abstract List<string> GetQuestionAboutItem { get; }
    }
}