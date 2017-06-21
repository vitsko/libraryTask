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
        private int year;
        private int number;
        private DateTime date;
        private string issn;

        public Newspaper()
        {
            this.ToConstructorForXML();
        }

        public Newspaper(List<string> aboutItemCatalog)
        {
            this.ToConstructor(aboutItemCatalog, CountOfData);
        }

        //[XmlElement(Order = 4)]
        public string PublisherCity { get; set; }

        //[XmlElement(Order = 5)]
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

        //[XmlElement(Order = 6)]
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
                    this.errorList.Add(string.Format(Titles.YearError, this.year));
                }
            }
        }

        //[XmlElement(Order = 7)]
        public int Number
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
                    this.number = Newspaper.DefaultNumber;
                    this.errorList.Add(string.Format(Titles.NumberNewsError, this.number));
                }
            }
        }

        //[XmlElement(Order = 8)]
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
                    this.date = DateTime.Today;
                    this.errorList.Add(string.Format(Titles.DateNewsError, this.date.ToShortDateString()));
                }
            }
        }

        //[XmlElement(Order = 9)]
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
                    this.issn = Newspaper.ISSNDefault;
                    this.errorList.Add(string.Format(Titles.ISSNError, this.issn));
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

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, string.Format(Titles.SaveType, (byte)Helper.TypeItem.Newspaper));
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            this.errorList.Clear();

            var intValue = 0d;
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

        protected override void CreateFromXML()
        {
            base.CreateFromXML();

            var intValue = 0d;
            DateTime date;

            this.PublisherCity = Helper.GetValueElement(Helper.CurrentNode, Titles.NameForCity);
            this.Publisher = Helper.GetValueElement(Helper.CurrentNode, Titles.NameForPublisher);

            Helper.IsMoreThanZero(Helper.GetValueElement(Helper.CurrentNode, Titles.NameForYear), out intValue);
            this.Year = (int)intValue;

            Helper.IsMoreThanZero(Helper.GetValueElement(Helper.CurrentNode, Titles.NameForNumber), out intValue);
            this.Number = (int)intValue;

            Helper.IsDate(Helper.GetValueElement(Helper.CurrentNode, Titles.NameForDate), out date);
            this.Date = date;

            this.ISSN = Helper.GetValueElement(Helper.CurrentNode, Titles.NameForISSN);

            Helper.CurrentNode = Helper.CurrentNode.NextSibling;
        }
    }
}