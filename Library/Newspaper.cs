namespace Library
{
    using System;

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

        public string ISSIN { get; set; }

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
            this.ISSIN = aboutItemCatalog[8];
        }
    }
}
