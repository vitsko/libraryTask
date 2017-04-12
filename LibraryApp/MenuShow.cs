using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
    internal static class MenuShow
    {
        internal static void AboutCatalog(string infoCatalog)
        {
            Console.Clear();
            Console.WriteLine(Display.ShowCatalog);
            Console.WriteLine(infoCatalog);
            Console.WriteLine(Display.PressAnyKey);
            Console.ReadKey();
        }

    }
}
