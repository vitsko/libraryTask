using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Library";

            while (true)
            {
                var selectMainMenu = Console.ReadKey();


                switch (selectMainMenu.KeyChar)
                {
                    case '2':
                        {
                            MenuToAdd.Draw();

                            var selectCreateditem = Console.ReadKey();

                            switch (selectCreateditem.KeyChar)
                            {
                                case '1':
                                    {
                                        Catalog.Library.Add(new Library.Book());
                                        break;
                                    }

                                default: break;
                            }

                            break;
                        }

                    default: break;
                }
            }
        }
    }
}
