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

        public int Year { get; set; }

        public int Number { get; set; }

        public string Date { get; set; }

        public string ISSN { get; set; }

        //internal override int PublishedYear
        //{
        //    get
        //    {
        //        int yearParse;
        //        bool result = int.TryParse(this.Year, out yearParse);

        //        if (result && yearParse > 0)
        //        {
        //            return yearParse;
        //        }

        //        return DateTime.Today.Year;
        //    }
        //}

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

            //if (this.Year == string.Empty)
            //{
            //    allinfo.AppendLine(this.PublishedYear.ToString());
            //}
            //else
            //{
            allinfo.AppendLine(this.Year.ToString());
            //}

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount.ToString());

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            allinfo.AppendLine(InfoObject.Number);
            allinfo.AppendLine(this.Number.ToString());

            allinfo.AppendLine(InfoObject.Date);
            allinfo.AppendLine(this.Date);

            allinfo.AppendLine(InfoObject.ISSN);
            allinfo.AppendLine(this.ISSN);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            this.Id = ItemCatalog.GetId();
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
