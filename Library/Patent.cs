namespace Library
{
    using System;
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

        public override string ToString()
        {
            StringBuilder allinfo = new StringBuilder();

            allinfo.AppendLine(InfoObject.TypePatent);

            allinfo.AppendLine(InfoObject.Title);
            allinfo.AppendLine(this.Title);

            allinfo.AppendLine(InfoObject.Inventors);
            allinfo.AppendLine(String.Join(Helper.Comma.ToString(), this.Inventors));

            allinfo.AppendLine(InfoObject.Country);
            allinfo.AppendLine(this.Country);

            allinfo.AppendLine(InfoObject.RegNumber);
            allinfo.AppendLine(this.RegNumber);

            allinfo.AppendLine(InfoObject.DateRequest);
            allinfo.AppendLine(this.DateRequest);

            allinfo.AppendLine(InfoObject.DatePublication);
            allinfo.AppendLine(this.DatePublication);

            allinfo.AppendLine(InfoObject.PageCount);
            allinfo.AppendLine(this.PageCount);

            allinfo.AppendLine(InfoObject.Note);
            allinfo.AppendLine(this.Note);

            return allinfo.ToString();
        }
    }
}
