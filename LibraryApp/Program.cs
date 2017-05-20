namespace LibraryApp
{
    using System;
    using Library;
    using System.Collections.Generic;
    using Resource;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "Library";
            bool exitMainMenu = false;

            while (!exitMainMenu)
            {
                Console.Clear();
                Screen.ShowText(Titles.MainMenu);
                var selectMainMenu = Console.ReadKey().Key;

                switch (selectMainMenu)
                {
                    case ConsoleKey.D1:
                        {
                            bool exitMenuAdd = false;
                            var isCorrectAdd = false;

                            ConsoleKey selectCreateditem = ConsoleKey.Q;

                            List<string> aboutItemCatalog;

                            while (!exitMenuAdd)
                            {
                                Console.Clear();
                                Screen.ShowText(Titles.MenuToAdd);
                                selectCreateditem = Console.ReadKey().Key;
                                aboutItemCatalog = Screen.Input(selectCreateditem);

                                switch (selectCreateditem)
                                {
                                    case ConsoleKey.D1:
                                        {
                                            isCorrectAdd = Catalog.Add(new Library.Book(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.D2:
                                        {
                                            isCorrectAdd = Catalog.Add(new Library.Newspaper(aboutItemCatalog));
                                            exitMenuAdd = true;
                                            break;
                                        }

                                    case ConsoleKey.D3:
                                        {
                                            isCorrectAdd = Catalog.Add(new Library.Patent(aboutItemCatalog));
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

                            if (selectCreateditem == ConsoleKey.D1 || selectCreateditem == ConsoleKey.D2 || selectCreateditem == ConsoleKey.D3)
                            {
                                Console.Clear();
                                Screen.ShowResultAddOrEdit(isCorrectAdd, Catalog.AllItem[Catalog.Count - 1]);
                                Console.ReadKey();
                            }

                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0)
                            {
                                var isCorrectEdit = false;

                                Screen.ShowText(Titles.AskToEdit);
                                var indexToEdit = Console.ReadLine();

                                int id = -1;
                                bool isInt = int.TryParse(indexToEdit, out id);

                                Console.Clear();

                                if (isInt && Catalog.AllItem.Exists(item => item.Id == id))
                                {
                                    var itemForEdit = Catalog.AllItem.Find(item => item.Id == id);

                                    Screen.ShowText(Titles.AboutEdit, itemForEdit.ToString(), Titles.Split);

                                    var aboutItem = Screen.InputInfoAboutItemCatalog(itemForEdit.GetQuestionAboutItem);

                                    isCorrectEdit = Catalog.Edit(itemForEdit, aboutItem);
                                    Screen.ShowResultAddOrEdit(isCorrectEdit, itemForEdit);
                                }
                                else
                                {
                                    Screen.ShowText(string.Format(Titles.NoExist, indexToEdit), Titles.PressAnyKey);
                                }
                            }
                            else
                            {
                                Screen.ShowText(Titles.EmptyCatalog);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0)
                            {
                                Screen.ShowText(Titles.AskToDelete);

                                var indexToDelete = Console.ReadLine();

                                int id = -1;
                                bool isInt = int.TryParse(indexToDelete, out id);

                                Console.Clear();

                                if (isInt)
                                {
                                    if (Catalog.Delete(id))
                                    {
                                        Screen.ShowText(string.Format(Titles.Delete, indexToDelete), Titles.PressAnyKey);
                                    }
                                    else
                                    {
                                        Screen.ShowText(string.Format(Titles.NoExist, indexToDelete), Titles.PressAnyKey);
                                    }
                                }
                                else
                                {
                                    Screen.ShowText(string.Format(Titles.NoExist, indexToDelete), Titles.PressAnyKey);
                                }
                            }
                            else
                            {
                                Screen.ShowText(Titles.EmptyCatalog);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0)
                            {
                                var info = Catalog.GetInfoCatalog();
                                Screen.ShowText(Titles.ShowCatalog, info, Titles.PressAnyKey);
                            }
                            else
                            {
                                Screen.ShowText(Titles.EmptyCatalog);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D5:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0)
                            {
                                Screen.ShowText(Titles.InputSeachRequest);
                                var searchTitle = Console.ReadLine();

                                List<ItemCatalog> resultCatalog;
                                var info = string.Empty;

                                if (searchTitle != string.Empty)
                                {
                                    resultCatalog = Catalog.GetItemWithTitle(searchTitle);
                                    info = Catalog.GetInfoSelectedItem(resultCatalog);
                                }

                                Console.Clear();
                                Screen.ShowText(string.Format(Titles.ResultSearchOfTitle, searchTitle), info, Titles.PressAnyKey);
                            }
                            else
                            {
                                Screen.ShowText(Titles.EmptyCatalog);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D6:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0)
                            {
                                bool exitSortYear = false;
                                List<ItemCatalog> catalogAfterSort = new List<ItemCatalog>();
                                var info = string.Empty;

                                ConsoleKey selectSortMenu = ConsoleKey.Q;

                                while (!exitSortYear)
                                {
                                    Console.Clear();
                                    Screen.ShowText(Titles.MenuToSortYear);
                                    selectSortMenu = Console.ReadKey().Key;

                                    switch (selectSortMenu)
                                    {
                                        case ConsoleKey.D1:
                                            catalogAfterSort = Catalog.SortByYearASC();
                                            exitSortYear = true;
                                            break;

                                        case ConsoleKey.D2:
                                            catalogAfterSort = Catalog.SortByYearDESC();
                                            exitSortYear = true;
                                            break;

                                        case ConsoleKey.Q:
                                            exitSortYear = true;
                                            break;

                                        default:
                                            break;
                                    }
                                }

                                if (selectSortMenu == ConsoleKey.D1 || selectSortMenu == ConsoleKey.D2)
                                {
                                    Console.Clear();
                                    info = Catalog.GetInfoSelectedItem(catalogAfterSort);
                                    Screen.ShowText(Titles.ShowCatalog, info, Titles.PressAnyKey);
                                }
                            }
                            else
                            {
                                Screen.ShowText(Titles.EmptyCatalog);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D7:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0 && Catalog.HasBook)
                            {
                                Screen.ShowText(Titles.InputSeachRequest);
                                var searchByAuthor = Console.ReadLine();
                                List<ItemCatalog> booksbyAuthor;
                                var info = string.Empty;

                                if (searchByAuthor != string.Empty)
                                {
                                    booksbyAuthor = Catalog.GetBookByAuthor(searchByAuthor);
                                    info = Catalog.GetInfoSelectedItem(booksbyAuthor);
                                }

                                Console.Clear();
                                Screen.ShowText(string.Format(Titles.ResultSearchOfAuthors, searchByAuthor), info, Titles.PressAnyKey);
                            }
                            else
                            {
                                Screen.ShowText(Titles.CatalogIsEmptyOrHasNotBook);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D8:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0 && Catalog.HasBook)
                            {
                                Screen.ShowText(Titles.InputSeachRequest);
                                var searchByPublisher = Console.ReadLine();
                                dynamic books;
                                var info = string.Empty;

                                if (searchByPublisher != string.Empty)
                                {
                                    books = Catalog.GroupBooksByPublisher(searchByPublisher);
                                    info = Catalog.GetInfoItemByGrouping(books, Catalog.GroupingBy.Publisher);
                                }

                                Console.Clear();
                                Screen.ShowText(string.Format(Titles.ResultGroupByPublisher, searchByPublisher), info, Titles.PressAnyKey);
                            }
                            else
                            {
                                Screen.ShowText(Titles.CatalogIsEmptyOrHasNotBook);
                            }

                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D9:
                        {
                            Console.Clear();

                            if (Catalog.Count != 0)
                            {
                                var groupedCatalogByYear = Catalog.GroupByYear();
                                var info = Catalog.GetInfoItemByGrouping(groupedCatalogByYear, Catalog.GroupingBy.Year);
                                Screen.ShowText(Titles.ResultGroupByYear, info, Titles.PressAnyKey);
                            }
                            else
                            {
                                Screen.ShowText(Titles.EmptyCatalog);
                            }

                            Console.ReadKey();
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
