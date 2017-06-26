namespace Library
{
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    using Helper;
    using Resource;

    public class Book : ItemCatalog
    {
        private const string ISBNDefault = "978-3-16-148410-0";
        private const int DefaultYear = 2000;
        private const int YearPublication = 1900;
        private const int CountOfData = 8;
        private List<string> authors;
        private string publisherCity;
        private string publisher;
        private int year;
        private string isbn;

        public Book()
        {
            this.CreateFromXML();
        }

        public Book(List<string> aboutItemCatalog)
        {
            this.ToConstructor(aboutItemCatalog, CountOfData);
        }

        [XmlArray(ElementName = "Authors", Order = 4), XmlArrayItem(ElementName = "Author")]
        public List<string> Authors
        {
            get
            {
                return this.authors;
            }

            set
            {
                if (Helper.XmlRead is XmlReader)
                {
                    this.authors = value;
                }
                else
                {
                    if (value.Count != 0)
                    {
                        this.authors = value;
                    }
                    else
                    {
                        this.authors = new List<string>(1);
                        this.GetDefAndErrorForArray(this.authors, Titles.DefaultAuthor, Titles.AuthorsError);
                    }
                }
            }
        }

        [XmlElement(Order = 5)]
        public string PublisherCity
        {
            get
            {
                return this.publisherCity;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.publisherCity = value;
                }
                else
                {
                    this.publisherCity = this.GetDefValueAndError(Titles.DefaultCity, string.Format(Titles.CityError, Titles.DefaultCity));
                }
            }
        }

        [XmlElement(Order = 6)]
        public string Publisher
        {
            get
            {
                return this.publisher;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.publisher = value;
                }
                else
                {
                    this.publisher = this.GetDefValueAndError(Titles.DefaultPublisher, string.Format(Titles.PublisherError, Titles.DefaultPublisher));
                }
            }
        }

        [XmlElement(Order = 7)]
        public int Year
        {
            get
            {
                return this.year;
            }

            set
            {
                if (value >= Book.YearPublication)
                {
                    this.year = value;
                }
                else
                {
                    this.year = this.GetDefValueAndError(Book.DefaultYear, string.Format(Titles.YearError, Book.DefaultYear));
                }
            }
        }

        [XmlElement(Order = 8)]
        public string ISBN
        {
            get
            {
                return this.isbn;
            }

            set
            {
                if (Helper.IsISBN(value))
                {
                    this.isbn = value;
                }
                else
                {
                    this.isbn = this.GetDefValueAndError(Book.ISBNDefault, string.Format(Titles.ISBNError, Book.ISBNDefault));
                }
            }
        }

        [XmlIgnore]
        public override string TypeItem
        {
            get
            {
                return Titles.TypeBook;
            }
        }

        [XmlIgnore]
        public override List<string> GetQuestionAboutItem
        {
            get
            {
                return Helper.GetyQuestions(Titles.AskAboutBook);
            }
        }

        internal override int PublishedYear
        {
            get
            {
                return this.Year;
            }
        }

        protected override Dictionary<string, string> TitleasAndValuesItem
        {
            get
            {
                Dictionary<string, string> titleasAndValuesItem = new Dictionary<string, string>();

                titleasAndValuesItem.Add(Titles.Title, this.Title);
                titleasAndValuesItem.Add(Titles.Authors, string.Join(ItemCatalog.Comma.ToString(), this.Authors));
                titleasAndValuesItem.Add(Titles.City, this.PublisherCity);
                titleasAndValuesItem.Add(Titles.Publisher, this.Publisher);
                titleasAndValuesItem.Add(Titles.Year, this.Year.ToString());
                titleasAndValuesItem.Add(Titles.PageCount, this.PageCount.ToString());
                titleasAndValuesItem.Add(Titles.Note, this.Note);
                titleasAndValuesItem.Add(Titles.ISBN, this.ISBN);

                return titleasAndValuesItem;
            }
        }

        public override string ToString()
        {
            return base.ToString()
                       .Insert(
                       0,
                       string.Format(Titles.AboutItem, this.Id.ToString(), Titles.TypeBook));
        }

        public override void CheckFromXML()
        {
            base.CheckFromXML();

            if (this.Authors.Count == 0)
            {
                this.authors = new List<string>(1);
                this.GetDefAndErrorForArray(this.authors, Titles.DefaultAuthor, Titles.AuthorsError);
            }

            if (this.PublisherCity == null)
            {
                this.PublisherCity = this.GetDefValueAndError(Titles.DefaultCity, string.Format(Titles.CityError, Titles.DefaultCity));
            }

            if (this.Publisher == null)
            {
                this.Publisher = this.GetDefValueAndError(Titles.DefaultPublisher, string.Format(Titles.PublisherError, Titles.DefaultPublisher));
            }

            if (this.Year < Book.YearPublication)
            {
                this.Year = this.GetDefValueAndError(Book.DefaultYear, string.Format(Titles.YearError, Book.DefaultYear));
            }

            if (this.ISBN == null)
            {
                this.ISBN = this.GetDefValueAndError(Book.ISBNDefault, string.Format(Titles.ISBNError, Book.ISBNDefault));
            }
        }

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, string.Format(Titles.SaveType, (byte)Helper.TypeItem.Book));
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            var authors = new List<string>(Helper
                                          .DeleteWhitespace(aboutItemCatalog[1])
                                          .Split(ItemCatalog.Comma));

            var intValue = 0d;

            this.Title = aboutItemCatalog[0];
            this.Authors = Helper.DeleteEmpty(authors);

            this.PublisherCity = aboutItemCatalog[2];
            this.Publisher = aboutItemCatalog[3];

            Helper.IsMoreThanZero(aboutItemCatalog[4], out intValue);
            this.Year = (int)intValue;

            Helper.IsMoreThanZero(aboutItemCatalog[5], out intValue);
            this.PageCount = (int)intValue;

            this.Note = aboutItemCatalog[6];
            this.ISBN = aboutItemCatalog[7];
        }
    }
}