using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Newspaper
    {
        public Newspaper()
        {
        }

        public string PublisherCity { get; set; }
        public string Publisher { get; set; }
        public DateTime Year { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSIN { get; set; }
    }
}
