using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book : ItemCatalog
    {
        public Book()
        {
        }

        public string[] Authors { get; set; }
        public string PublisherCity { get; set; }
        public string Publisher { get; set; }
        public DateTime Year { get; set; }
        public string ISBN { get; set; }
    }
}
