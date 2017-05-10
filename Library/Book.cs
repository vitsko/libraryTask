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

        public int Year { get; set; }

        public string ISBN { get; set; }

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

            allinfo.AppendLine(ItemCatalog.Charp + this.Id.ToString() + InfoObject.TypeBook);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(InfoObject.Authors);
            allinfo.AppendLine(string.Join(COMMA.ToString(), this.Authors));

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
            // }

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount.ToString());

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            allinfo.AppendLine(InfoObject.ISBN);
            allinfo.AppendLine(this.ISBN);

            return allinfo.ToString();
        }

        protected override void Create(string[] aboutItemCatalog)
        {
            this.Id = ItemCatalog.GetId();
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
