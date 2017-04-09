namespace Library
{
    using System;

    public class Book : ItemCatalog
    {
           public Book(string[] aboutItemCatalog)
        {
            this.Create(aboutItemCatalog);
        }

        public string[] Authors { get; set; }

        public string PublisherCity { get; set; }

        public string Publisher { get; set; }

        public string Year { get; set; }

        public string ISBN { get; set; }

        protected override void Create(string[] aboutItemCatalog)
        {
            this.Title = aboutItemCatalog[0];
            this.Authors = Helper
                           .DeleteWhitespace(aboutItemCatalog[1])
                           .Split(Helper.Comma, StringSplitOptions.RemoveEmptyEntries);
            this.PublisherCity = aboutItemCatalog[2];
            this.Publisher = aboutItemCatalog[3];
            this.Year = aboutItemCatalog[4];
            this.PageCount = aboutItemCatalog[5];
            this.Note = aboutItemCatalog[6];
            this.ISBN = aboutItemCatalog[7];
        }
    }
}
