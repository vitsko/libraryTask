namespace Library
{
    using System.Collections.Generic;
    using Helper;
    using Resource;

    public class Book : ItemCatalog
    {
        private const string ISBNDefault = "0000000000000";
        private const int DefaultYear = 2000;
        private const int YearPublication = 1900;
        private const int CountOfData = 8;
        private List<string> authors;
        private string publisherCity;
        private string publisher;
        private int year;
        private string isbn;

        public Book(List<string> aboutItemCatalog)
        {
            this.ToConstructor(aboutItemCatalog, CountOfData);
        }

        public List<string> Authors
        {
            get
            {
                return this.authors;
            }

            set
            {
                if (value.Count != 0)
                {
                    this.authors = value;
                }
                else
                {
                    this.authors = new List<string>(1);
                    this.authors.Add(Titles.DefaultAuthor);
                    this.errorList.Add(string.Format(Titles.AuthorsError, this.authors[0]));
                }
            }
        }

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
                    this.publisherCity = Titles.DefaultCity;
                    this.errorList.Add(string.Format(Titles.CityError, this.publisherCity));
                }
            }
        }

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
                    this.publisher = Titles.DefaultPublisher;
                    this.errorList.Add(string.Format(Titles.PublisherError, this.publisher));
                }
            }
        }

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
                    this.year = Book.DefaultYear;
                    this.errorList.Add(string.Format(Titles.YearError, this.year));
                }
            }
        }

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
                    this.isbn = Book.ISBNDefault;
                    this.errorList.Add(string.Format(Titles.ISBNError, this.isbn));
                }
            }
        }

        public override string TypeItem
        {
            get
            {
                return Titles.TypeBook;
            }
        }

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

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, string.Format(Titles.SaveType, (byte)Helper.TypeItem.Book));
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            this.errorList.Clear();

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