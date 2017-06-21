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
            this.ToConstructorForXML();
        }

        public Patent(List<string> aboutItemCatalog)
        {
            this.ToConstructor(aboutItemCatalog, CountOfData);
        }

        [XmlArray("Inventors"/*, Order = 4*/), XmlArrayItem("Inventor")]
        public List<string> Inventors
        {
            get
            {
                return this.inventors;
            }

            set
            {
                if (value.Count != 0)
                {
                    this.inventors = value;
                }
                else
                {
                    this.inventors = new List<string>(1);
                    this.inventors.Add(Titles.DefaultInventor);
                    this.errorList.Add(string.Format(Titles.InventorError, this.inventors[0]));
                }
            }
        }

        //[XmlElement(Order = 5)]
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
                    this.сountry = Titles.DefaultCountry;
                    this.errorList.Add(string.Format(Titles.CountryError, this.сountry));
                }
            }
        }

        //[XmlElement(Order = 6)]
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
                    this.regNumber = Patent.defaultRegNumber;
                    this.errorList.Add(string.Format(Titles.PatentRegNumberError, this.regNumber));
                }
            }
        }

        //[XmlElement(Order = 7)]
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
                    this.dateRequest = Patent.defaultDate;
                    this.errorList.Add(string.Format(Titles.DateRPatentError, this.dateRequest.ToShortDateString()));
                }
            }
        }

        //[XmlElement(Order = 8)]
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
                    this.datePublication = Patent.defaultDate;
                    this.errorList.Add(string.Format(Titles.DatePPatentError, this.datePublication.ToShortDateString()));
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

        internal override string GetInfoToSave()
        {
            return base.GetInfoToSave().Insert(0, string.Format(Titles.SaveType, (byte)Helper.TypeItem.Patent));
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            this.errorList.Clear();

            var inventors = new List<string>(Helper
                           .DeleteWhitespace(aboutItemCatalog[1])
                           .Split(ItemCatalog.Comma));

            var intValue = 0d;
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
            this.PageCount = (int)intValue;

            this.Note = aboutItemCatalog[7];
        }

        protected override void CreateFromXML()
        {
            base.CreateFromXML();
            DateTime date;

            this.Inventors = Helper.GetListValues(Titles.NameForInventors, Titles.NameForInventor);

            this.Country = Helper.GetValueElement(Helper.CurrentNode, Titles.NameForCountry);
            this.RegNumber = Helper.GetValueElement(Helper.CurrentNode, Titles.NameForRegNumber);

            Helper.IsDate(Helper.GetValueElement(Helper.CurrentNode, Titles.NameForDateRequest), out date);
            this.DateRequest = date;

            Helper.IsDate(Helper.GetValueElement(Helper.CurrentNode, Titles.NameForDatePublication), out date);
            this.DatePublication = date;

            Helper.CurrentNode = Helper.CurrentNode.NextSibling;
        }
    }
}