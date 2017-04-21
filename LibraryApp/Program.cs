namespace LibraryApp
{
    using System;
    using Library;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Library";
            bool exitMainMenu = false;

            while (!exitMainMenu)
            {
                MainMenu.Draw();
                var selectMainMenu = Console.ReadKey();

                switch (selectMainMenu.Key)
                {
                    case ConsoleKey.D1:
                        {
                            bool exitMenuAdd = false;

                            while (!exitMenuAdd)
                            {
                                MenuToAdd.Draw();

                                var selectCreateditem = Console.ReadKey();

                                var aboutItemCatalog = MenuToAdd.Input(selectCreateditem);

                                switch (selectCreateditem.Key)
                                {
                                    case ConsoleKey.D1:
                                        {
                                            Catalog.Library.Add(new Library.Book(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.D2:
                                        {
                                            Catalog.Library.Add(new Library.Newspaper(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.D3:
                                        {
                                            Catalog.Library.Add(new Library.Patent(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.Q:
                                        {
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    default:
                                        break;
                                }
                            }

                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            if (Catalog.Count != 0)
                            {
                                MenuToDelete.Draw();
                                var indexToDelete = Console.ReadLine();

                                int index = -1;
                                bool isInt = int.TryParse(indexToDelete, out index);

                                if (isInt)
                                {
                                    var isDelete = Catalog.Library.Delete(index);

                                    if (isDelete)
                                    {
                                        MenuToDelete.AboutDelete(indexToDelete);
                                    }
                                    else
                                    {
                                        MenuToDelete.NotDelete(indexToDelete);
                                    }
                                }
                                else
                                {
                                    MenuToDelete.NotDelete(indexToDelete);
                                }
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            if (Catalog.Count != 0)
                            {
                                var info = Catalog.Library.GetInfoCatalog();
                                MenuShow.AboutCatalog(info);
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            if (Catalog.Count != 0)
                            {
                                MenuShow.InputSeachRequest();
                                var searchTitle = Console.ReadLine();
                                var info = string.Empty;

                                if (searchTitle != string.Empty)
                                {
                                    info = Catalog.Library.GetInfoItemWithTitle(searchTitle);
                                }

                                MenuShow.ResultSearchOfTitle(searchTitle, info);
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D5:
                        {
                            if (Catalog.Count != 0)
                            {
                                bool exitSortYear = false;
                                var info = string.Empty;

                                while (!exitSortYear)
                                {
                                    MenuSortAndGroup.Draw();
                                    var selectSortMenu = Console.ReadKey();

                                    switch (selectSortMenu.KeyChar)
                                    {
                                        case '1':
                                            info = Catalog.Library.SortByYearAsc();
                                            exitSortYear = true;
                                            break;

                                        case '2':
                                            info = Catalog.Library.SortByYearDesc();
                                            exitSortYear = true;
                                            break;

                                        case 'q':
                                            exitSortYear = true;
                                            break;

                                        default:
                                            break;
                                    }

                                    if (info != string.Empty)
                                    {
                                        MenuShow.AboutCatalog(info);
                                    }
                                }
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D6:
                        {
                            if (Catalog.Count != 0)
                            {
                                MenuShow.InputSeachRequest();
                                var searchByAuthor = Console.ReadLine();
                                var info = string.Empty;

                                if (searchByAuthor != string.Empty)
                                {
                                    info = Catalog.Library.InfoBookByAuthor(searchByAuthor);
                                }

                                MenuShow.ResultSearchByAuthors(searchByAuthor, info);
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D7:
                        {
                            if (Catalog.Count != 0)
                            {
                                MenuShow.InputSeachRequest();
                                var searchByPublisher = Console.ReadLine();
                                var info = string.Empty;

                                if (searchByPublisher != string.Empty)
                                {
                                    info = Catalog.Library.GroupBooksByPublisher(searchByPublisher);
                                }

                                MenuShow.ResultGroupByPublisher(searchByPublisher, info);
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D8:
                        {
                            if (Catalog.Count != 0)
                            {
                                var info = Catalog.Library.GroupByYear();
                                MenuShow.GroupByYear(info);
                            }
                            else
                            {
                                MenuToDelete.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.Q:
                        {
                            exitMainMenu = true;
                            break;
                        }

                    default: break;
                }
            }
        }
    }
}
