namespace Library
{
    using System;
    using System.Text;

    public class Book : ItemCatalog
    {
        private const char COMMA = ',';

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

        public override string ToString()
        {
            StringBuilder allinfo = new StringBuilder();

            allinfo.AppendLine(InfoObject.TypeBook);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(InfoObject.Authors);
            allinfo.AppendLine(String.Join(COMMA.ToString(), this.Authors));

            allinfo.AppendLine(InfoObject.City);
            allinfo.AppendLine(this.PublisherCity);

            allinfo.AppendLine(InfoObject.Publisher);
            allinfo.AppendLine(this.Publisher);

            allinfo.AppendLine(InfoObject.Year);
            allinfo.AppendLine(this.Year);

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount);

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            allinfo.AppendLine(InfoObject.ISBN);
            allinfo.AppendLine(this.ISBN);

            return allinfo.ToString();
        }
    }
}
