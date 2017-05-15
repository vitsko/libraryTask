namespace Library
{
    using System;
    using System.Globalization;
    using System.Text;

    public class Patent : ItemCatalog
    {
        private static DateTime defaultDate = new DateTime(1950, 01, 01);
        private string[] inventors;
        private string сountry;
        private DateTime dateRequest;
        private DateTime datePublication;

        public Patent(string[] aboutItemCatalog)
        {
            errorList.Clear();
            this.Create(aboutItemCatalog);
        }

        public string[] Inventors
        {
            get
            {
                return this.inventors;
            }
            set
            {
                if (value.Length != 0)
                {
                    this.inventors = value;
                }
                else
                {
                    this.inventors = new string[1];
                    this.inventors[0] = AboutObject.DefaultInventor;
                    ItemCatalog.errorList.Add(AboutObject.InventorError + this.inventors[0]);
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
                    this.сountry = AboutObject.DefaultCountry;
                    ItemCatalog.errorList.Add(AboutObject.CountryError + this.сountry);
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
                    ItemCatalog.errorList.Add(AboutObject.DateRPatentError + this.dateRequest.ToShortDateString());
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
                    ItemCatalog.errorList.Add(AboutObject.DatePPatentError + this.datePublication.ToShortDateString());
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

            allinfo.AppendLine(ItemCatalog.Charp + this.Id.ToString() + InfoObject.TypePatent);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(InfoObject.Inventors);
            allinfo.AppendLine(string.Join(Helper.Comma.ToString(), this.Inventors));

            allinfo.AppendLine(InfoObject.Country);
            allinfo.AppendLine(this.Country);

            allinfo.AppendLine(InfoObject.RegNumber);
            allinfo.AppendLine(this.RegNumber);

            allinfo.AppendLine(InfoObject.DateRequest);
            allinfo.AppendLine(this.DateRequest.ToShortDateString());

            allinfo.AppendLine(InfoObject.DatePublication);
            allinfo.AppendLine(this.DatePublication.ToShortDateString());


            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount.ToString());

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            int intValue = 0;
            DateTime date;

            this.Id = ItemCatalog.GetId();
            this.Title = aboutItemCatalog[0];
            this.Inventors = Helper
                           .DeleteWhitespace(aboutItemCatalog[1])
                           .Split(Helper.Comma, StringSplitOptions.RemoveEmptyEntries);

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
    }
}
