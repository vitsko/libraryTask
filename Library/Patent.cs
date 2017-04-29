namespace Library
{
    using System;
    using System.Globalization;
    using System.Text;

    public class Patent : ItemCatalog
    {
        public Patent(string[] aboutItemCatalog)
        {
            this.Create(aboutItemCatalog);
        }

        public string[] Inventors { get; set; }

        public string Country { get; set; }

        public string RegNumber { get; set; }

        public string DateRequest { get; set; }

        public string DatePublication { get; set; }

        internal override int PublishedYear
        {
            get
            {
                CultureInfo newCulture = new CultureInfo("en-US", true);
                newCulture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";

                DateTime enterDate;

                bool result = DateTime.TryParseExact(this.DatePublication, newCulture.DateTimeFormat.ShortDatePattern, newCulture, DateTimeStyles.AllowWhiteSpaces, out enterDate);

                if (result)
                {
                    return enterDate.Year;
                }

                return DateTime.Today.Year;
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
            allinfo.AppendLine(this.DateRequest);

            allinfo.AppendLine(InfoObject.DatePublication);

            if (this.DatePublication == string.Empty)
            {
                allinfo.AppendLine(DateTime.Today.ToShortDateString());
            }
            else
            {
                allinfo.AppendLine(this.DatePublication);
            }

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount);

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            this.Id = ItemCatalog.GetId();
            this.Title = aboutItemCatalog[0];
            this.Inventors = Helper
                           .DeleteWhitespace(aboutItemCatalog[1])
                           .Split(Helper.Comma, StringSplitOptions.RemoveEmptyEntries);
            this.Country = aboutItemCatalog[2];
            this.RegNumber = aboutItemCatalog[3];
            this.DateRequest = aboutItemCatalog[4];
            this.DatePublication = aboutItemCatalog[5];
            this.PageCount = aboutItemCatalog[6];
            this.Note = aboutItemCatalog[7];
        }
    }
}
