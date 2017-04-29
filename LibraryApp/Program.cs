﻿namespace LibraryApp
{
    using System;
    using Library;
    using Test;
    using System.Collections.Generic;

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
                                            Catalog.Add(new Library.Book(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.D2:
                                        {
                                            Catalog.Add(new Library.Newspaper(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.D3:
                                        {
                                            Catalog.Add(new Library.Patent(aboutItemCatalog));
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

                                int id = -1;
                                bool isInt = int.TryParse(indexToDelete, out id);

                                if (isInt)
                                {
                                    if (Catalog.Delete(id))
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
                                MenuShow.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            if (Catalog.Count != 0)
                            {
                                var info = Catalog.GetInfoCatalog();
                                MenuShow.AboutCatalog(info);
                            }
                            else
                            {
                                MenuShow.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            if (Catalog.Count != 0)
                            {
                                MenuShow.InputSeachRequest();
                                var searchTitle = Console.ReadLine();

                                ItemCatalog[] resultCatalog;
                                var info = string.Empty;

                                if (searchTitle != string.Empty)
                                {
                                    resultCatalog = Catalog.GetItemWithTitle(searchTitle);
                                    info = Catalog.GetInfoSelectedItem(resultCatalog);
                                }

                                MenuShow.ResultSearchOfTitle(searchTitle, info);
                            }
                            else
                            {
                                MenuShow.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D5:
                        {
                            if (Catalog.Count != 0)
                            {
                                bool exitSortYear = false;
                                ItemCatalog[] catalogAfterSort;
                                var info = string.Empty;

                                while (!exitSortYear)
                                {
                                    MenuSortAndGroup.Draw();
                                    var selectSortMenu = Console.ReadKey();

                                    switch (selectSortMenu.Key)
                                    {
                                        case ConsoleKey.D1:
                                            catalogAfterSort = Catalog.SortByYearAsc();
                                            info = Catalog.GetInfoSelectedItem(catalogAfterSort);
                                            MenuShow.AboutCatalog(info);
                                            exitSortYear = true;
                                            break;

                                        case ConsoleKey.D2:
                                            catalogAfterSort = Catalog.SortByYearDesc();
                                            info = Catalog.GetInfoSelectedItem(catalogAfterSort);
                                            MenuShow.AboutCatalog(info);
                                            exitSortYear = true;
                                            break;

                                        case ConsoleKey.Q:
                                            exitSortYear = true;
                                            break;

                                        default:
                                            break;
                                    }

                                }
                            }
                            else
                            {
                                MenuShow.AboutEmptyCatalog();
                            }

                            break;
                        }

                    case ConsoleKey.D6:
                        {
                            if (Catalog.Count != 0 && Catalog.HasBook)
                            {
                                MenuShow.InputSeachRequest();
                                var searchByAuthor = Console.ReadLine();
                                ItemCatalog[] booksbyAuthor;
                                var info = string.Empty;

                                if (searchByAuthor != string.Empty)
                                {
                                    booksbyAuthor = Catalog.GetBookByAuthor(searchByAuthor);
                                    info = Catalog.GetInfoSelectedItem(booksbyAuthor);
                                }

                                MenuShow.ResultSearchByAuthors(searchByAuthor, info);
                            }
                            else
                            {
                                MenuShow.CatalogIsEmptyOrHasNotBook();
                            }

                            break;
                        }

                    case ConsoleKey.D7:
                        {
                            if (Catalog.Count != 0 && Catalog.HasBook)
                            {
                                MenuShow.InputSeachRequest();
                                var searchByPublisher = Console.ReadLine();
                                ItemCatalog[] books;
                                var info = string.Empty;

                                if (searchByPublisher != string.Empty)
                                {
                                    books = Catalog.GroupBooksByPublisher(searchByPublisher);
                                    info = Catalog.GetInfoSelectedItem(books);
                                }

                                MenuShow.ResultGroupByPublisher(searchByPublisher, info);
                            }
                            else
                            {
                                MenuShow.CatalogIsEmptyOrHasNotBook();
                            }

                            break;
                        }

                    case ConsoleKey.D8:
                        {
                            if (Catalog.Count != 0)
                            {
                                ItemCatalog[] groupedCatalogByYear = Catalog.GroupByYear();
                                var info = Catalog.GetInfoSelectedItem(groupedCatalogByYear);
                                MenuShow.GroupByYear(info);
                            }
                            else
                            {
                                MenuShow.AboutEmptyCatalog();
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
