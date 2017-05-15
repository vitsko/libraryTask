namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Book : ItemCatalog
    {
        private const char COMMA = ',';
        private const int DefaultYear = 2000;
        private const int YearPublication = 1900;
        private string[] authors;
        private string publisherCity;
        private string publisher;
        private int year;

        public Book(string[] aboutItemCatalog)
        {
            errorList.Clear();
            this.Create(aboutItemCatalog);
        }

        public string[] Authors
        {
            get
            {
                return this.authors;
            }
            set
            {
                if (value.Length != 0)
                {
                    this.authors = value;
                }
                else
                {
                    this.authors = new string[1];
                    this.authors[0] = AboutObject.DefaultAuthor;
                    ItemCatalog.errorList.Add(AboutObject.AuthorsError + this.authors[0]);
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
                    this.publisherCity = AboutObject.DefaultCity;
                    ItemCatalog.errorList.Add(AboutObject.CityError + this.publisherCity);
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
                    this.publisher = AboutObject.DefaultPublisher;
                    ItemCatalog.errorList.Add(AboutObject.PublisherError + this.publisher);
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
                    ItemCatalog.errorList.Add(AboutObject.YearError + this.year);
                }

            }
        }

        public string ISBN { get; set; }

        internal override int PublishedYear
        {
            get
            {
                return this.Year;
            }
        }

        public override string ToString()
        {
            StringBuilder allinfo = new StringBuilder();

            allinfo.AppendLine(ItemCatalog.Charp + this.Id.ToString() + InfoObject.TypeBook);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(InfoObject.Authors);
            allinfo.AppendLine(
                string.Join(COMMA.ToString(), this.Authors));

            allinfo.AppendLine(InfoObject.City);
            allinfo.AppendLine(this.PublisherCity);

            allinfo.AppendLine(InfoObject.Publisher);
            allinfo.AppendLine(this.Publisher);

            allinfo.AppendLine(InfoObject.Year);
            allinfo.AppendLine(this.Year.ToString());

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount.ToString());

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            allinfo.AppendLine(InfoObject.ISBN);
            allinfo.AppendLine(this.ISBN);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            var intValue = 0;

            this.Id = ItemCatalog.GetId();
            this.Title = aboutItemCatalog[0];
            this.Authors = Helper
                           .DeleteWhitespace(aboutItemCatalog[1])
                           .Split(Helper.Comma, StringSplitOptions.RemoveEmptyEntries);

            this.PublisherCity = aboutItemCatalog[2];
            this.Publisher = aboutItemCatalog[3];

            Helper.IsIntMoreThanZero(aboutItemCatalog[4], out intValue);
            this.Year = intValue;

            Helper.IsIntMoreThanZero(aboutItemCatalog[5], out intValue);
            this.PageCount = intValue;

            this.Note = aboutItemCatalog[6];
            this.ISBN = aboutItemCatalog[7];
        }
    }
}
