namespace Library
{
    using Helper;
    using Resource;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public class Patent : ItemCatalog
    {
        private static DateTime defaultDate = new DateTime(1950, 01, 01);
        private List<string> inventors;
        private string сountry;
        private DateTime dateRequest;
        private DateTime datePublication;

        public Patent(List<string> aboutItemCatalog)
        {
            this.Id = ItemCatalog.GetId();
            this.Create(aboutItemCatalog);
        }

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
                    ItemCatalog.errorList.Add(string.Format(Titles.InventorError, this.inventors[0]));
                }
            }
        }

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
                    ItemCatalog.errorList.Add(string.Format(Titles.CountryError, this.сountry));
                }
            }
        }

        public string RegNumber { get; set; }

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
                    ItemCatalog.errorList.Add(string.Format(Titles.DateRPatentError, this.dateRequest.ToShortDateString()));
                }

            }
        }

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
                    ItemCatalog.errorList.Add(string.Format(Titles.DatePPatentError, this.datePublication.ToShortDateString()));
                }

            }
        }

        internal override int PublishedYear
        {
            get
            {
                return this.DatePublication.Year;
            }
        }

        public override string ToString()
        {
            StringBuilder allinfo = new StringBuilder();

            allinfo.AppendLine(string.Format(Titles.AboutItem, ItemCatalog.Charp, this.Id.ToString(), Titles.TypePatent));

            allinfo.AppendLine(Titles.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(Titles.Inventors);
            allinfo.AppendLine(string.Join(ItemCatalog.Comma.ToString(), this.Inventors));

            allinfo.AppendLine(Titles.Country);
            allinfo.AppendLine(this.Country);

            allinfo.AppendLine(Titles.RegNumber);
            allinfo.AppendLine(this.RegNumber);

            allinfo.AppendLine(Titles.DateRequest);
            allinfo.AppendLine(this.DateRequest.ToShortDateString());

            allinfo.AppendLine(Titles.DatePublication);
            allinfo.AppendLine(this.DatePublication.ToShortDateString());


            allinfo.AppendLine(Titles.PageCount);
            allinfo.AppendLine(this.PageCount.ToString());

            allinfo.AppendLine(Titles.Note);
            allinfo.AppendLine(this.Note);

            return allinfo.ToString();
        }

        protected internal override void Create(List<string> aboutItemCatalog)
        {
            errorList.Clear();

            var inventors = new List<string>(Helper
                           .DeleteWhitespace(aboutItemCatalog[1])
                           .Split(ItemCatalog.Comma));

            int intValue = 0;
            DateTime date;

            this.Title = aboutItemCatalog[0];
            this.Inventors = Helper.DeleteEmpty(inventors);

            this.Country = aboutItemCatalog[2];
            this.RegNumber = aboutItemCatalog[3];

            Helper.IsDateAsDDMMYYYY(aboutItemCatalog[4], out date);
            this.DateRequest = date;

            Helper.IsDateAsDDMMYYYY(aboutItemCatalog[5], out date);
            this.DatePublication = date;

            Helper.IsIntMoreThanZero(aboutItemCatalog[6], out intValue);
            this.PageCount = intValue;

            this.Note = aboutItemCatalog[7];
        }

        public override List<string> GetQuestionAboutItem
        {
            get
            {
                return Helper.GetyQuestions(Titles.AskAboutPatent);
            }
        }
    }
}