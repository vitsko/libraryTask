namespace Library
{
    using Helper;
    using Resource;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class ItemCatalog : IComparable<ItemCatalog>
    {
        protected const char Comma = ',';
        protected const int DefaultPageCount = 100;
        protected List<string> errorList = new List<string>();

        private const char Sharp = '#';
        private const string SplitToSave = ": ";
        private const int countForBookAndPatent = 8;
        private const int countForNews = 9;

        private static int countOfItem = 0;
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
                    this.errorList.Add(string.Format(Titles.TitleError, this.title));
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
                    this.errorList.Add(string.Format(Titles.PageCountError, this.pageCount));
                }
            }
        }

        public string Note { get; set; }

        public int Id { get; protected set; }

        internal abstract int PublishedYear { get; }

        public abstract string TypeItem { get; }

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

        public bool IsCorrectCreating()
        {
            return this.errorList.Count == 0;
        }

        public List<string> GetListOfError()
        {
            return this.errorList;
        }

        public override string ToString()
        {
            StringBuilder allinfo = new StringBuilder();

            foreach (var item in this.TitleasAndValuesItem)
            {
                allinfo.AppendLine(item.Key);
                allinfo.AppendLine(item.Value);
            }

            return allinfo.ToString();
        }

        protected static int GetId()
        {
            return ++countOfItem;
        }

        protected internal abstract void Create(List<string> aboutItemCatalog);

        public abstract List<string> GetQuestionAboutItem { get; }

        protected abstract Dictionary<string, string> TitleasAndValuesItem { get; }

        internal virtual string GetInfoToSave()
        {
            StringBuilder toSave = new StringBuilder();

            foreach (var item in this.TitleasAndValuesItem)
            {
                toSave.Append(item.Key);
                toSave.Append(item.Value);
                toSave.Append(ItemCatalog.Sharp);
            }

            return toSave.ToString();
        }

        internal static ItemCatalog CreateFromFile(List<string> aboutItemCatalog)
        {
            var type = aboutItemCatalog.ElementAt(0);
            ItemCatalog fromImport = null;

            switch (Helper.GetTypeItem(type))
            {
                case (byte)Helper.TypeItem.Book:
                    {
                        Helper.AddValuesForCheckImport(aboutItemCatalog, ItemCatalog.countForBookAndPatent);
                        fromImport = new Book(aboutItemCatalog.FindAll(item => !item.Equals(type)));
                        break;
                    }

                case (byte)Helper.TypeItem.Newspaper:
                    {
                        Helper.AddValuesForCheckImport(aboutItemCatalog, ItemCatalog.countForNews);
                        fromImport = new Newspaper(aboutItemCatalog.FindAll(item => !item.Equals(type)));
                        break;
                    }

                case (byte)Helper.TypeItem.Patent:
                    {
                        Helper.AddValuesForCheckImport(aboutItemCatalog, ItemCatalog.countForBookAndPatent);
                        fromImport = new Patent(aboutItemCatalog.FindAll(item => !item.Equals(type)));
                        break;
                    }
            }

            return fromImport;
        }
    }
}