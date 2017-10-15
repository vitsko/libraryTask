namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Helper;
    using Resource;

    [XmlType(TypeName = "Item")]
    [XmlInclude(typeof(Book)), XmlInclude(typeof(Newspaper)), XmlInclude(typeof(Patent))]
    public abstract class ItemCatalog : IComparable<ItemCatalog>
    {
        protected const char Comma = ',';
        protected const int DefaultPageCount = 100;

        private const char Sharp = '#';
        private const string SplitToSave = ": ";
        private const int CountForBookAndPatent = 8;
        private const int CountForNews = 9;

        private static int countOfItem = 0;

        private List<string> errorList;
        private string title;
        private dynamic pageCount;

        [XmlElement(Order = 1)]
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
                    this.title = this.GetDefValueAndError(Titles.DefaultTitle, string.Format(Titles.TitleError, Titles.DefaultTitle));
                }
            }
        }

        [XmlElement(Order = 2)]
        public dynamic PageCount
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
                    this.pageCount = this.GetDefValueAndError(ItemCatalog.DefaultPageCount, string.Format(Titles.PageCountError, ItemCatalog.DefaultPageCount));
                }
            }
        }

        [XmlElement(Order = 3)]
        public string Note { get; set; }

        [XmlIgnore]
        public int Id { get; protected set; }

        [XmlIgnore]
        public List<string> ErrorList
        {
            get
            {
                return this.errorList;
            }
        }

        [XmlIgnore]
        public abstract string TypeItem { get; }

        [XmlIgnore]
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

        public virtual void CheckFromXML()
        {
            if (this.Title == null)
            {
                this.Title = this.GetDefValueAndError(Titles.DefaultTitle, string.Format(Titles.TitleError, Titles.DefaultTitle));
            }

            if (this.PageCount == 0)
            {
                this.PageCount = this.GetDefValueAndError(ItemCatalog.DefaultPageCount, string.Format(Titles.PageCountError, ItemCatalog.DefaultPageCount));
            }
        }

        internal static ItemCatalog CreateFromFile(List<string> aboutItemCatalog)
        {
            var type = aboutItemCatalog.ElementAt(0);
            dynamic typeByInt;

            if (Helper.IsMoreThanZero(type, out typeByInt))
            {
                var select = (Helper.TypeItem)typeByInt;

                var onlyData = aboutItemCatalog.FindAll(item => !item.Equals(type));

                return select == Helper.TypeItem.Book ? Book.CreateItem(onlyData) :
                       select == Helper.TypeItem.Newspaper ? Newspaper.CreateItem(onlyData) :
                       select == Helper.TypeItem.Patent ? Patent.CreateItem(onlyData) : null;
            }

            return null;
        }

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

        protected internal abstract void Create(List<string> aboutItemCatalog);

        protected static int GetId()
        {
            return ++countOfItem;
        }

        protected void CreateFromXML()
        {
            this.Id = ItemCatalog.GetId();
            this.errorList = new List<string>();
        }

        protected void ToConstructor(List<string> aboutItemCatalog, int countOfData)
        {
            Helper.AddStringsForCheck(aboutItemCatalog, countOfData);
            this.Id = ItemCatalog.GetId();
            this.errorList = new List<string>();
            this.Create(aboutItemCatalog);
        }

        protected dynamic GetDefValueAndError(dynamic defaultValue, string errorMessage)
        {
            this.errorList.Add(errorMessage);

            return defaultValue;
        }

        protected void GetDefAndErrorForArray(List<string> athorsOrInventors, string defaultValue, string message)
        {
            athorsOrInventors.Add(defaultValue);
            this.errorList.Add(string.Format(message, athorsOrInventors[0]));
        }
    }
}