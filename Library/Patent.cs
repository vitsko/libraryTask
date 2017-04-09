namespace Library
{
    using System;

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

        protected override void Create(string[] aboutItemCatalog)
        {
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
