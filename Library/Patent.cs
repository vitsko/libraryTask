namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Serialization;
    using Helper;
    using Resource;

    public class Patent : ItemCatalog
    {
        private const int CountOfData = 8;
        private static string defaultRegNumber = string.Format(Titles.DefaultRegNumber, "1", DateTime.Today.Year, "1");
        private static DateTime defaultDate = new DateTime(1950, 01, 01);
        private List<string> inventors;
        private string сountry;
        private DateTime dateRequest;
        private DateTime datePublication;
        private string regNumber;

        public Patent()
        {
            this.CreateFromXML();
        }

        public Patent(List<string> aboutItemCatalog)
        {
            this.ToConstructor(aboutItemCatalog, Patent.CountOfData);
        }

        [XmlArray("Inventors", Order = 4), XmlArrayItem("Inventor")]
        public List<string> Inventors
        {
            get
            {
                return this.inventors;
            }

            set
            {
                if (Helper.XmlRead is XmlReader)
                {
                    this.inventors = value;
                }
                else
                {
                    if (value.Count != 0)
                    {
                        this.inventors = value;
                    }
                    else
                    {
                        this.inventors = new List<string>(1);
                        this.GetDefAndErrorForArray(this.inventors, Titles.DefaultInventor, Titles.InventorError);
                    }
                }
            }
        }

        [XmlElement(Order = 5)]
        public string Country
        {
            get
            {
                return this.сountry;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.сountry = value;
                }
                else
                {
                    this.сountry = this.GetDefValueAndError(Titles.DefaultCountry, string.Format(Titles.CountryError, Titles.DefaultCountry));
                }
            }
        }

        [XmlElement(Order = 6)]
        public string RegNumber
        {
            get
            {
                return this.regNumber;
            }

            set
            {
                if (Helper.IsRegNum(value))
                {
                    this.regNumber = value;
                }
                else
                {
                    this.regNumber = this.GetDefValueAndError(Patent.defaultRegNumber, string.Format(Titles.PatentRegNumberError, Patent.defaultRegNumber));
                }
            }
        }

        [XmlElement(Order = 7)]
        public DateTime DateRequest
        {
            get
            {
                return this.dateRequest;
            }

            set
            {
                if (value >= Patent.defaultDate)
                {
                    this.dateRequest = value;
                }
                else
                {
                    this.dateRequest = this.GetDefValueAndError(Patent.defaultDate, string.Format(Titles.DateRPatentError, Patent.defaultDate.ToShortDateString()));
                }
            }
        }

        [XmlElement(Order = 8)]
        public DateTime DatePublication
        {
            get
            {
                return this.datePublication;
            }

            set
            {
                if (value >= Patent.defaultDate)
                {
                    this.datePublication = value;
                }
                else
                {
                    this.datePublication = this.GetDefValueAndError(Patent.defaultDate, string.Format(Titles.DatePPatentError, Patent.defaultDate.ToShortDateString()));
                }
            }
        }

        [XmlIgnore]
        public override string TypeItem
        {
            get
            {
                return Titles.TypePatent;
            }
        }

        [XmlIgnore]
        public override List<string> GetQuestionAboutItem
        {
            get
            {
                return Helper.GetyQuestions(Titles.AskAboutPatent);
            }
        }

        internal override int PublishedYear
        {
            get
            {
                return this.DatePublication.Year;
            }
        }

        protected override Dictionary<string, string> TitleasAndValuesItem
        {
            get
            {
                Dictionary<string, string> titleasAndValuesItem = new Dictionary<string, string>();

                titleasAndValuesItem.Add(Titles.Title, this.Title);
                titleasAndValuesItem.Add(Titles.Inventors, string.Join(ItemCatalog.Comma.ToString(), this.Inventors));
                titleasAndValuesItem.Add(Titles.Country, this.Country);
                titleasAndValuesItem.Add(Titles.RegNumber, this.RegNumber);
                titleasAndValuesItem.Add(Titles.DateRequest, this.DateRequest.ToShortDateString());
                titleasAndValuesItem.Add(Titles.DatePublication, this.DatePublication.ToShortDateString());
                titleasAndValuesItem.Add(Titles.PageCount, this.PageCount.ToString());
                titleasAndValuesItem.Add(Titles.Note, this.Note);

                return titleasAndValuesItem;
            }
        }

        public override string ToString()
        {
            return base.ToString()
                       .Insert(
                       0,
                       string.Format(Titles.AboutItem, this.Id.ToString(), Titles.TypePatent));
        }

        public override void CheckFromXML()
        {
            base.CheckFromXML();

            if (this.Inventors.Count == 0)
            {
                this.inventors = new List<string>(1);
                this.GetDefAndErrorForArray(this.inventors, Titles.DefaultInventor, Titles.InventorError);
            }

            if (this.Country == null)
            {
                this.Country = this.GetDefValueAndError(Titles.DefaultCountry, string.Format(Titles.CountryError, Titles.DefaultCountry));
            }

            if (this.RegNumber == null)
            {
                this.RegNumber = this.GetDefValueAndError(Patent.defaultRegNumber, string.Format(Titles.PatentRegNumberError, Patent.defaultRegNumber));
            }

            if (this.DateRequest < Patent.defaultDate)
            {
                this.DateRequest = this.GetDefValueAndError(Patent.defaultDate, string.Format(Titles.DateRPatentError, Patent.defaultDate.ToShortDateString()));
            }

            if (this.DatePublication < Patent.defaultDate)
            {
                this.DatePublication = this.GetDefValueAndError(Patent.defaultDate, string.Format(Titles.DatePPatentError, Patent.defaultDate.ToShortDateString()));
            }
        }

        internal static ItemCatalog CreateItem(List<string> onlyData)
        {
            return new Patent(onlyData);
        }

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, string.Format(Titles.SaveType, (byte)Helper.TypeItem.Patent));
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            var inventors = new List<string>(Helper
                 .DeleteWhitespace(aboutItemCatalog[1])
                 .Split(ItemCatalog.Comma));

            dynamic intValue;
            DateTime date;

            this.Title = aboutItemCatalog[0];
            this.Inventors = Helper.DeleteEmpty(inventors);

            this.Country = aboutItemCatalog[2];
            this.RegNumber = aboutItemCatalog[3];

            Helper.IsDate(aboutItemCatalog[4], out date);
            this.DateRequest = date;

            Helper.IsDate(aboutItemCatalog[5], out date);
            this.DatePublication = date;

            Helper.IsMoreThanZero(aboutItemCatalog[6], out intValue);
            this.PageCount = intValue;

            this.Note = aboutItemCatalog[7];
        }
    }
}