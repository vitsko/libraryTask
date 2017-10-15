namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    using Helper;
    using Resource;

    public class Newspaper : ItemCatalog
    {
        private const string ISSNDefault = "0378-5955";
        private const int YearPublication = 1900;
        private const int DefaultYear = 2000;
        private const byte DefaultNumber = 1;
        private const int CountOfData = 9;
        private string publisher;
        private dynamic year;
        private dynamic number;
        private DateTime date;
        private string issn;

        public Newspaper()
        {
            this.CreateFromXML();
        }

        public Newspaper(List<string> aboutItemCatalog)
        {
            this.ToConstructor(aboutItemCatalog, Newspaper.CountOfData);
        }

        [XmlElement(Order = 4)]
        public string PublisherCity { get; set; }

        [XmlElement(Order = 5)]
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

        [XmlElement(Order = 6)]
        public dynamic Year
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
                    this.year = this.GetDefValueAndError(Newspaper.DefaultYear, string.Format(Titles.YearError, Newspaper.DefaultYear));
                }
            }
        }

        [XmlElement(Order = 7)]
        public dynamic Number
        {
            get
            {
                return this.number;
            }

            set
            {
                if (value > 0)
                {
                    this.number = value;
                }
                else
                {
                    this.number = this.GetDefValueAndError(Newspaper.DefaultNumber, string.Format(Titles.NumberNewsError, Newspaper.DefaultNumber));
                }
            }
        }

        [XmlElement(Order = 8)]
        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                if (value != DateTime.MinValue)
                {
                    this.date = value;
                }
                else
                {
                    this.date = this.GetDefValueAndError(DateTime.Today, string.Format(Titles.DateNewsError, DateTime.Today.ToShortDateString()));
                }
            }
        }

        [XmlElement(Order = 9)]
        public string ISSN
        {
            get
            {
                return this.issn;
            }

            set
            {
                if (Helper.IsISSN(value))
                {
                    this.issn = value;
                }
                else
                {
                    this.issn = this.GetDefValueAndError(Newspaper.ISSNDefault, string.Format(Titles.ISSNError, Newspaper.ISSNDefault));
                }
            }
        }

        [XmlIgnore]
        public override string TypeItem
        {
            get
            {
                return Titles.TypeNews;
            }
        }

        [XmlIgnore]
        public override List<string> GetQuestionAboutItem
        {
            get
            {
                return Helper.GetyQuestions(Titles.AskAboutNewspaper);
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
                titleasAndValuesItem.Add(Titles.City, this.PublisherCity);
                titleasAndValuesItem.Add(Titles.Publisher, this.Publisher);
                titleasAndValuesItem.Add(Titles.Year, this.Year.ToString());
                titleasAndValuesItem.Add(Titles.PageCount, this.PageCount.ToString());
                titleasAndValuesItem.Add(Titles.Note, this.Note);
                titleasAndValuesItem.Add(Titles.Number, this.Number.ToString());
                titleasAndValuesItem.Add(Titles.Date, this.Date.ToShortDateString());
                titleasAndValuesItem.Add(Titles.ISSN, this.ISSN);

                return titleasAndValuesItem;
            }
        }

        public override string ToString()
        {
            return base.ToString()
                       .Insert(
                       0,
                       string.Format(Titles.AboutItem, this.Id.ToString(), Titles.TypeNews));
        }

        public override void CheckFromXML()
        {
            base.CheckFromXML();

            if (this.Publisher == null)
            {
                this.Publisher = this.GetDefValueAndError(Titles.DefaultPublisher, string.Format(Titles.PublisherError, Titles.DefaultPublisher));
            }

            if (this.Year < Newspaper.YearPublication)
            {
                this.Year = this.GetDefValueAndError(Newspaper.DefaultYear, string.Format(Titles.YearError, Newspaper.DefaultYear));
            }

            if (this.Number < 0)
            {
                this.Number = this.GetDefValueAndError(Newspaper.DefaultNumber, string.Format(Titles.NumberNewsError, Newspaper.DefaultNumber));
            }

            if (this.ISSN == null)
            {
                this.ISSN = this.GetDefValueAndError(Newspaper.ISSNDefault, string.Format(Titles.ISSNError, Newspaper.ISSNDefault));
            }
        }

        internal static ItemCatalog CreateItem(List<string> onlyData)
        {
            return new Newspaper(onlyData);
        }

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, string.Format(Titles.SaveType, (byte)Helper.TypeItem.Newspaper));
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            dynamic intValue;
            DateTime date;

            this.Title = aboutItemCatalog[0];
            this.PublisherCity = aboutItemCatalog[1];
            this.Publisher = aboutItemCatalog[2];

            Helper.IsMoreThanZero(aboutItemCatalog[3], out intValue);
            this.Year = (int)intValue;

            Helper.IsMoreThanZero(aboutItemCatalog[4], out intValue);
            this.PageCount = (int)intValue;

            this.Note = aboutItemCatalog[5];

            Helper.IsMoreThanZero(aboutItemCatalog[6], out intValue);
            this.Number = (int)intValue;

            Helper.IsDate(aboutItemCatalog[7], out date);
            this.Date = date;

            this.ISSN = aboutItemCatalog[8];
        }
    }
}