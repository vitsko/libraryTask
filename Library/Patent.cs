using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Patent
    {
        public Patent() { }

        public string[] Inventors { get; set; }
        public string Country { get; set; }
        public string RegNumber { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime DatePublication { get; set; }
    }
}
