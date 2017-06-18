﻿namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Helper;
    using Resource;

    public abstract class ItemCatalog : IComparable<ItemCatalog>
    {
        protected const char Comma = ',';
        protected const int DefaultPageCount = 100;
        protected List<string> errorList;

        private const char Sharp = '#';
        private const string SplitToSave = ": ";
        private const int CountForBookAndPatent = 8;
        private const int CountForNews = 9;

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
                if (value > 0)
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

        public List<string> ErrorList
        {
            get
            {
                return this.errorList;
            }
        }

        public abstract string TypeItem { get; }

        public abstract List<string> GetQuestionAboutItem { get; }

        internal abstract int PublishedYear { get; }

        protected abstract Dictionary<string, string> TitleasAndValuesItem { get; }

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
            return this.ErrorList.Count == 0;
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

        internal static ItemCatalog CreateFromFile(List<string> aboutItemCatalog)
        {
            var type = aboutItemCatalog.ElementAt(0);
            var typeByInt = 0d;

            ItemCatalog toCatalog = null;

            if (Helper.IsMoreThanZero(type, out typeByInt))
            {
                switch ((byte)typeByInt)
                {
                    case (byte)Helper.TypeItem.Book:
                        return new Book(aboutItemCatalog.FindAll(item => !item.Equals(type)));


                    case (byte)Helper.TypeItem.Newspaper:
                        return new Newspaper(aboutItemCatalog.FindAll(item => !item.Equals(type)));


                    case (byte)Helper.TypeItem.Patent:
                        return new Patent(aboutItemCatalog.FindAll(item => !item.Equals(type)));
                }
            }

            return toCatalog;
        }

        internal virtual string GetInfoToSave()
        {
            StringBuilder toSave = new StringBuilder();

            foreach (var item in this.TitleasAndValuesItem)
            {
                toSave.Append(item.Key);
                ///toSave.Append("\x0022");
                toSave.Append(item.Value);
                ///toSave.Append("\x0022");
                toSave.Append(ItemCatalog.Sharp);
            }

            return toSave.ToString();
        }

        protected internal abstract void Create(List<string> aboutItemCatalog);

        protected static int GetId()
        {
            return ++countOfItem;
        }

        protected void ToConstructor(List<string> aboutItemCatalog, int CountOfData)
        {
            Helper.AddStringsForCheck(aboutItemCatalog, CountOfData);
            this.Id = ItemCatalog.GetId();
            this.errorList = new List<string>();
            this.Create(aboutItemCatalog);
        }
    }
}