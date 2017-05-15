namespace Library
{
    using System;
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

        public Newspaper(string[] aboutItemCatalog)
        {
            errorList.Clear();
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
                if (value >= Newspaper.YearPublication)
                {
                    this.year = value;
                }
                else
                {
                    this.year = Newspaper.DefaultYear;
                    ItemCatalog.errorList.Add(AboutObject.YearError + this.year);
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
                    ItemCatalog.errorList.Add(AboutObject.NumberNewsError + this.number);
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
                    ItemCatalog.errorList.Add(AboutObject.DateNewsError + this.date.ToShortDateString());
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

            allinfo.AppendLine(ItemCatalog.Charp + this.Id.ToString() + InfoObject.TypeNews);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

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

            allinfo.AppendLine(InfoObject.Number);
            allinfo.AppendLine(this.Number.ToString());

            allinfo.AppendLine(InfoObject.Date);
            allinfo.AppendLine(this.Date.ToShortDateString());

            allinfo.AppendLine(InfoObject.ISSN);
            allinfo.AppendLine(this.ISSN);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            var intValue = 0;
            DateTime date;

            this.Id = ItemCatalog.GetId();
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
    }
}
