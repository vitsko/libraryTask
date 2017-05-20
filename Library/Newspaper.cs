namespace Library
{
    using Helper;
    using Resource;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Newspaper : ItemCatalog
    {
        private const int YearPublication = 1900;
        private const int DefaultYear = 2000;
        private const byte DefaultNumber = 1;
        private string publisher;
        private int year;
        private int number;
        private DateTime date;

        public Newspaper(List<string> aboutItemCatalog)
        {
            this.Id = ItemCatalog.GetId();
            this.Create(aboutItemCatalog);
        }

        public string PublisherCity { get; set; }

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
                    ItemCatalog.errorList.Add(Titles.PublisherError + this.publisher);
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
                if (value >= Newspaper.YearPublication)
                {
                    this.year = value;
                }
                else
                {
                    this.year = Newspaper.DefaultYear;
                    ItemCatalog.errorList.Add(Titles.YearError + this.year);
                }

            }
        }

        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value > 0)
                {
                    this.number = value;
                }
                else
                {
                    this.number = Newspaper.DefaultNumber;
                    ItemCatalog.errorList.Add(Titles.NumberNewsError + this.number);
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (value != DateTime.MinValue)
                {
                    this.date = value;
                }
                else
                {
                    this.date = DateTime.Today;
                    ItemCatalog.errorList.Add(Titles.DateNewsError + this.date.ToShortDateString());
                }
            }
        }

        public string ISSN { get; set; }

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

            allinfo.AppendLine(string.Format(Titles.AboutItem, ItemCatalog.Charp, this.Id.ToString(), Titles.TypeNews));

            allinfo.AppendLine(Titles.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(Titles.City);
            allinfo.AppendLine(this.PublisherCity);

            allinfo.AppendLine(Titles.Publisher);
            allinfo.AppendLine(this.Publisher);

            allinfo.AppendLine(Titles.Year);
            allinfo.AppendLine(this.Year.ToString());


            allinfo.AppendLine(Titles.PageCount);
            allinfo.AppendLine(this.PageCount.ToString());

            allinfo.AppendLine(Titles.Note);
            allinfo.AppendLine(this.Note);

            allinfo.AppendLine(Titles.Number);
            allinfo.AppendLine(this.Number.ToString());

            allinfo.AppendLine(Titles.Date);
            allinfo.AppendLine(this.Date.ToShortDateString());

            allinfo.AppendLine(Titles.ISSN);
            allinfo.AppendLine(this.ISSN);

            return allinfo.ToString();
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            errorList.Clear();

            var intValue = 0;
            DateTime date;

            this.Title = aboutItemCatalog[0];
            this.PublisherCity = aboutItemCatalog[1];
            this.Publisher = aboutItemCatalog[2];

            Helper.IsIntMoreThanZero(aboutItemCatalog[3], out intValue);
            this.Year = intValue;

            Helper.IsIntMoreThanZero(aboutItemCatalog[4], out intValue);
            this.PageCount = intValue;

            this.Note = aboutItemCatalog[5];

            Helper.IsIntMoreThanZero(aboutItemCatalog[6], out intValue);
            this.Number = intValue;

            Helper.IsDateAsDDMMYYYY(aboutItemCatalog[7], out date);
            this.Date = date;

            this.ISSN = aboutItemCatalog[8];
        }

        public override List<string> GetQuestionAboutItem
        {
            get
            {
                return Helper.GetyQuestions(Titles.AskAboutNewspaper);
            }
        }
    }
}