namespace LibraryApp
{
    using System;
    using Helper;
    using Library;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.AccessControl;
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

                            if (selectCreateditem == ConsoleKey.D1 || selectCreateditem == ConsoleKey.D2 || selectCreateditem == ConsoleKey.D3)
                            {
                                Console.Clear();
                                Screen.ShowResultAddOrEdit(Catalog.AllItem[Catalog.Count - 1]);
                                Console.ReadKey();
                            }

                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();

                            if (Catalog.IsNotEmpty)
                            {
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

                                    Catalog.Edit(itemForEdit, aboutItem);
                                    Screen.ShowResultAddOrEdit(itemForEdit);
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

                            if (Catalog.IsNotEmpty)
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

                            if (Catalog.IsNotEmpty)
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

                            if (Catalog.IsNotEmpty)
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

                            if (Catalog.IsNotEmpty)
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

                            if (Catalog.IsNotEmpty && Catalog.HasBook)
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

                            if (Catalog.IsNotEmpty && Catalog.HasBook)
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

                            if (Catalog.IsNotEmpty)
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

                    case ConsoleKey.D0:
                        {
                            Console.Clear();

                            Screen.ShowText(Titles.MenuToFiles);

                            bool exitMenuFiles = false;
                            string fullPath = string.Format(Titles.FullPath, Environment.CurrentDirectory, Titles.CatalogToFile);

                            ConsoleKey selectSortMenu = ConsoleKey.Q;

                            while (!exitMenuFiles)
                            {
                                Console.Clear();
                                Screen.ShowText(Titles.MenuToFiles);
                                selectSortMenu = Console.ReadKey().Key;

                                Console.Clear();

                                switch (selectSortMenu)
                                {
                                    case ConsoleKey.D1:
                                        {
                                            if (Catalog.IsNotEmpty)
                                            {
                                                var folder = Directory.CreateDirectory(fullPath);

                                                StreamWriter writer = new StreamWriter(Titles.FileToSaveWithPath);

                                                writer.Write(Catalog.Save());
                                                writer.Close();

                                                Screen.ShowText(string.Format(Titles.MessageOfSaveFile, fullPath, Titles.FileToSave));
                                            }
                                            else
                                            {
                                                Screen.ShowText(Titles.EmptyCatalog);
                                            }

                                            exitMenuFiles = true;

                                            Console.ReadKey();
                                            break;
                                        }

                                    case ConsoleKey.D2:
                                        {
                                            if (File.Exists(Titles.FileToSaveWithPath))
                                            {
                                                Screen.ShowText(Titles.MenuToImport);

                                                bool exitMenuImport = false;

                                                while (!exitMenuImport)
                                                {
                                                    Console.Clear();
                                                    Screen.ShowText(Titles.MenuToImport);
                                                    selectSortMenu = Console.ReadKey().Key;
                                                    StreamReader reader = null;
                                                    List<List<string>> stringsFromFile = null;

                                                    if (selectSortMenu == ConsoleKey.D1 || selectSortMenu == ConsoleKey.D2)
                                                    {
                                                        reader = new StreamReader(Titles.FileToSaveWithPath);
                                                        stringsFromFile = new List<List<string>>();
                                                    }

                                                    switch (selectSortMenu)
                                                    {
                                                        case ConsoleKey.D1:
                                                            {
                                                                Console.Clear();

                                                                bool resultImport = false;
                                                                int countOfString = 0;

                                                                while (reader.Peek() >= 0)
                                                                {
                                                                    var aboutItem = reader.ReadLine();
                                                                    countOfString++;

                                                                    List<string> afterParsing;

                                                                    if (Helper.IsTypeOfItemCatalog(aboutItem, out afterParsing))
                                                                    {
                                                                        stringsFromFile.Add(Helper.ParseStringToItem(afterParsing));
                                                                    }
                                                                    else
                                                                    {
                                                                        Screen.ShowText(string.Format(Titles.NoParseItem, fullPath, Titles.FileToSave));
                                                                        break;
                                                                    }
                                                                }

                                                                reader.Close();

                                                                if (countOfString == stringsFromFile.Count)
                                                                {
                                                                    resultImport = Catalog.LoadWithoutError(stringsFromFile);

                                                                    if (resultImport)
                                                                    {
                                                                        Screen.ShowText(string.Format(Titles.CorrectImport, fullPath, Titles.FileToSave));
                                                                    }
                                                                    else
                                                                    {
                                                                        Screen.ShowText(string.Format(Titles.ItemIsIncorrectInFile, fullPath, Titles.FileToSave));
                                                                    }
                                                                }

                                                                exitMenuImport = true;

                                                                Console.ReadKey();
                                                                break;
                                                            }

                                                        case ConsoleKey.D2:
                                                            {
                                                                Console.Clear();

                                                                var writer = new StreamWriter(Titles.FileToLogWithPath);

                                                                while (reader.Peek() >= 0)
                                                                {
                                                                    var aboutItem = reader.ReadLine();

                                                                    List<string> afterParsing;

                                                                    if (Helper.IsTypeOfItemCatalog(aboutItem, out afterParsing))
                                                                    {
                                                                        stringsFromFile.Add(Helper.ParseStringToItem(afterParsing));
                                                                    }
                                                                    else
                                                                    {
                                                                        writer.WriteLine(Titles.AboutIncorrectTypeItem);
                                                                        writer.WriteLine(aboutItem);

                                                                        continue;
                                                                    }
                                                                }

                                                                reader.Close();

                                                                if (stringsFromFile.Count != 0)
                                                                {
                                                                    Catalog.Load(stringsFromFile);
                                                                    writer.Write(Screen.ResultImportToLog());
                                                                }

                                                                writer.Close();

                                                                Screen.ShowText(string.Format(Titles.CorrectImportWithLog, fullPath, Titles.FileToSave, Titles.FileOfLog));

                                                                exitMenuImport = true;

                                                                Console.ReadKey();
                                                                break;
                                                            }

                                                        case ConsoleKey.Q:
                                                            exitMenuImport = true;
                                                            break;

                                                        default:
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Screen.ShowText(string.Format(Titles.NoExistFile, fullPath, Titles.FileToSave));
                                                Console.ReadKey();
                                            }

                                            exitMenuFiles = true;
                                            break;
                                        }

                                    case ConsoleKey.Q:
                                        exitMenuFiles = true;
                                        break;

                                    default:
                                        break;
                                }
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
