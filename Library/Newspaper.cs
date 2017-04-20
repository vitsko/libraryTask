namespace Library
{
    using System;
    using System.Text;

    public class Newspaper : ItemCatalog
    {
        public Newspaper(string[] aboutItemCatalog)
        {
            this.Create(aboutItemCatalog);
        }

        public string PublisherCity { get; set; }

        public string Publisher { get; set; }

        public string Year { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }

        public string ISSN { get; set; }

        internal override int PublishedYear
        {
            get
            {
                int yearParse;
                bool result = int.TryParse(this.Year, out yearParse);

                if (result && yearParse > 0)
                {
                    return yearParse;
                }

                return DateTime.Today.Year;
            }
        }

        public override string ToString()
        {
            StringBuilder allinfo = new StringBuilder();

            allinfo.AppendLine(InfoObject.TypeNews);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(InfoObject.City);
            allinfo.AppendLine(this.PublisherCity);

            allinfo.AppendLine(InfoObject.Publisher);
            allinfo.AppendLine(this.Publisher);

            allinfo.AppendLine(InfoObject.Year);

            if (this.Year == string.Empty)
            {
                allinfo.AppendLine(this.PublishedYear.ToString());
            }
            else
            {
                allinfo.AppendLine(this.Year);
            }

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount);

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            allinfo.AppendLine(InfoObject.Number);
            allinfo.AppendLine(this.Number);

            allinfo.AppendLine(InfoObject.Date);
            allinfo.AppendLine(this.Date);

            allinfo.AppendLine(InfoObject.ISSN);
            allinfo.AppendLine(this.ISSN);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            this.Title = aboutItemCatalog[0];
            this.PublisherCity = aboutItemCatalog[1];
            this.Publisher = aboutItemCatalog[2];
            this.Year = aboutItemCatalog[3];
            this.PageCount = aboutItemCatalog[4];
            this.Note = aboutItemCatalog[5];
            this.Number = aboutItemCatalog[6];
            this.Date = aboutItemCatalog[7];
            this.ISSN = aboutItemCatalog[8];
        }
    }
}
