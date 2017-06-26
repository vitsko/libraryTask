namespace LibraryApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Helper;
    using Library;
    using Resource;

    internal static class MainMenu
    {
        private static dynamic selectedKey = new
        {
            ConsoleKey.D1,
            ConsoleKey.D2,
            ConsoleKey.D3,
            ConsoleKey.NumPad1,
            ConsoleKey.NumPad2,
            ConsoleKey.NumPad3
        };

        internal static void Add()
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
                    case ConsoleKey.NumPad1:
                        {
                            Catalog.Add(new Library.Book(aboutItemCatalog));
                            exitMenuAdd = true;
                            break;
                        }

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            Catalog.Add(new Library.Newspaper(aboutItemCatalog));
                            exitMenuAdd = true;
                            break;
                        }

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
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

            if (MainMenu.ConditionToAdd(selectCreateditem))
            {
                Console.Clear();
                Screen.ShowResultAddOrEdit(Catalog.AllItem[Catalog.Count - 1]);
                Console.ReadKey();
            }
        }

        internal static void Edit()
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
        }

        internal static void Delete()
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
        }

        internal static void ShowCatalog()
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
        }

        internal static void SearchByTitle()
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
                    var seacher = Searcher.GetItemWithTitle;

                    resultCatalog = Searcher.Search(seacher, searchTitle, Catalog.AllItem);
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
        }

        internal static void SortByYear()
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

                    Sorter.Sorting sortByYear = null;

                    switch (selectSortMenu)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:

                            sortByYear = Sorter.SortByYearASC;
                            catalogAfterSort = Sorter.Sort(sortByYear, Catalog.AllItem);

                            exitSortYear = true;
                            break;

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:

                            sortByYear = Sorter.SortByYearDESC;
                            catalogAfterSort = Sorter.Sort(sortByYear, Catalog.AllItem);

                            exitSortYear = true;
                            break;

                        case ConsoleKey.Q:
                            exitSortYear = true;
                            break;

                        default:
                            break;
                    }
                }

                if (MainMenu.ConditionToAdd(selectSortMenu))
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
        }

        internal static void SearchBooksByAuthor()
        {
            Console.Clear();

            if (Catalog.IsNotEmpty && Catalog.HasBook)
            {
                Screen.ShowText(Titles.InputSeachRequest);
                var searchByAuthor = Console.ReadLine();
                var info = string.Empty;

                if (searchByAuthor != string.Empty)
                {
                    var seacher = Searcher.GetBookByAuthor;
                    var booksbyAuthor = Searcher.Search(seacher, searchByAuthor, Catalog.OnlyBooks);

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
        }

        internal static void SearchBooksByPublisher()
        {
            Console.Clear();

            if (Catalog.IsNotEmpty && Catalog.HasBook)
            {
                Screen.ShowText(Titles.InputSeachRequest);
                var searchByPublisher = Console.ReadLine();
                var info = string.Empty;

                if (searchByPublisher != string.Empty)
                {
                    var seacher = Searcher.GroupBooksByPublisher;
                    var books = Searcher.Search(seacher, searchByPublisher, Catalog.OnlyBooks);

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
        }

        internal static void GroupByYear()
        {
            Console.Clear();

            if (Catalog.IsNotEmpty)
            {
                var groupedCatalogByYear = Sorter.GroupByYear;
                var resultGrouping = Sorter.Sort(groupedCatalogByYear, Catalog.AllItem);
                var info = Catalog.GetInfoItemByGrouping(resultGrouping, Catalog.GroupingBy.Year);
                Screen.ShowText(Titles.ResultGroupByYear, info, Titles.PressAnyKey);
            }
            else
            {
                Screen.ShowText(Titles.EmptyCatalog);
            }

            Console.ReadKey();
        }

        internal static void WorkWithStorage()
        {
            Console.Clear();

            Screen.ShowText(Titles.MenuToFiles);

            bool exitMenuFiles = false;
            string fullPath = string.Format(Titles.FullPath, Environment.CurrentDirectory, Titles.CatalogToFile);

            ConsoleKey selectMenu = ConsoleKey.Q;

            while (!exitMenuFiles)
            {
                Console.Clear();
                Screen.ShowText(Titles.MenuToFiles);
                selectMenu = Console.ReadKey().Key;

                Console.Clear();

                switch (selectMenu)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        {
                            MainMenu.SaveToFile(fullPath, out exitMenuFiles);
                            break;
                        }

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            MainMenu.Import(selectMenu, out exitMenuFiles, fullPath);
                            break;
                        }

                    case ConsoleKey.Q:
                        exitMenuFiles = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private static bool ConditionToAdd(ConsoleKey selectCreateditem)
        {
            return selectCreateditem == selectedKey.D1 ||
                   selectCreateditem == selectedKey.D2 ||
                   selectCreateditem == selectedKey.D3 ||
                   selectCreateditem == selectedKey.NumPad1 ||
                   selectCreateditem == selectedKey.NumPad2 ||
                   selectCreateditem == selectedKey.NumPad3;
        }

        private static void SaveToFile(string fullPath, out bool exitMenuFiles)
        {
            if (Catalog.IsNotEmpty)
            {
                bool exitToUp = false;
                Screen.ShowText(Titles.SavedTypeMunu);
                ConsoleKey savedType = ConsoleKey.Q;

                while (!exitToUp)
                {
                    Console.Clear();
                    Screen.ShowText(Titles.SavedTypeMunu);
                    savedType = Console.ReadKey().Key;

                    switch (savedType)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            {
                                Console.Clear();

                                var folder = Directory.CreateDirectory(fullPath);

                                StreamWriter writer = new StreamWriter(Titles.FileToSaveWithPath);

                                writer.Write(Catalog.Save());
                                writer.Close();

                                Screen.ShowText(string.Format(Titles.MessageOfSaveFile, fullPath, Titles.FileTXT));
                                exitToUp = true;

                                Console.ReadKey();
                                break;
                            }

                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            {
                                Console.Clear();

                                var serializer = new XmlSerializer(typeof(List<ItemCatalog>));
                                StreamWriter writer = new StreamWriter(Titles.PathWithXMLtoSave);
                                serializer.Serialize(writer, Catalog.AllItem);
                                writer.Close();

                                Screen.ShowText(string.Format(Titles.MessageOfSaveFile, fullPath, Titles.FileXML));
                                exitToUp = true;

                                Console.ReadKey();
                                break;
                            }

                        case ConsoleKey.Q:
                            exitToUp = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                Screen.ShowText(Titles.EmptyCatalog);
                Console.ReadKey();
            }

            exitMenuFiles = true;
        }

        private static void Import(ConsoleKey selectMenu, out bool exitMenuFiles, string fullPath)
        {
            StreamReader reader = null;

            if (File.Exists(Titles.PathWithXMLtoSave))
            {
                var serializer = new XmlSerializer(typeof(List<ItemCatalog>));
                XmlReader xmlReader = null;

                try
                {
                    xmlReader = XmlReader.Create(Titles.PathWithXMLtoSave);
                    Helper.XmlRead = xmlReader;

                    var catalog = (List<ItemCatalog>)serializer.Deserialize(Helper.XmlRead);

                    Catalog.RewriteCatalog(catalog);
                    Screen.ShowText(string.Format(Titles.ResultLoadXML, fullPath, Titles.FileXML));
                }
                catch (FileNotFoundException ex)
                {
                    Screen.ShowText(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Screen.ShowText(string.Format(Titles.ErrorParseXML, fullPath, Titles.FileXML), ex.Message, ex.InnerException.Message);
                }
                finally
                {
                    if (xmlReader != null)
                    {
                        xmlReader.Close();
                        Helper.XmlRead = null;
                    }
                }
            }
            else
            {
                Screen.ShowText(Titles.MenuToImport);

                bool exitMenuImport = false;

                try
                {
                    while (!exitMenuImport)
                    {
                        Console.Clear();
                        Screen.ShowText(Titles.MenuToImport);
                        selectMenu = Console.ReadKey().Key;

                        List<List<string>> stringsFromFile = null;

                        if (MainMenu.ConditionToAdd(selectMenu))
                        {
                            reader = new StreamReader(Titles.FileToSaveWithPath);
                            stringsFromFile = new List<List<string>>();
                        }

                        switch (selectMenu)
                        {
                            case ConsoleKey.D1:
                            case ConsoleKey.NumPad1:
                                {
                                    MainMenu.ImportWithoutError(reader, stringsFromFile, fullPath, out exitMenuImport);
                                    break;
                                }

                            case ConsoleKey.D2:
                            case ConsoleKey.NumPad2:
                                {
                                    MainMenu.ImportWithError(reader, stringsFromFile, fullPath, out exitMenuImport);
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
                catch (FileNotFoundException ex)
                {
                    Console.Clear();
                    Screen.ShowText(ex.Message);
                }
            }

            Screen.ShowText(Titles.PressAnyKey);
            Console.ReadKey();
            exitMenuFiles = true;
        }

        private static void ImportWithoutError(StreamReader reader, List<List<string>> stringsFromFile, string fullPath, out bool exitMenuImport)
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
                    Screen.ShowText(string.Format(Titles.NoParseItem, fullPath, Titles.FileTXT));
                    break;
                }
            }

            reader.Close();

            if (stringsFromFile.Count != 0)
            {
                if (stringsFromFile.Count == countOfString)
                {
                    resultImport = Catalog.LoadWithoutError(stringsFromFile);

                    if (resultImport)
                    {
                        Screen.ShowText(string.Format(Titles.CorrectImport, fullPath, Titles.FileTXT));
                    }
                    else
                    {
                        Screen.ShowText(string.Format(Titles.ItemIsIncorrectInFile, fullPath, Titles.FileTXT));
                    }
                }
            }
            else
            {
                Screen.ShowText(string.Format(Titles.EmptyFile, fullPath, Titles.FileTXT));
            }

            exitMenuImport = true;
        }

        private static void ImportWithError(StreamReader reader, List<List<string>> stringsFromFile, string fullPath, out bool exitMenuImport)
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
                Screen.ShowText(string.Format(Titles.CorrectImportWithLog, fullPath, Titles.FileTXT, Titles.FileOfLog));
            }
            else
            {
                Screen.ShowText(string.Format(Titles.EmptyFile, fullPath, Titles.FileTXT));
            }

            writer.Close();

            exitMenuImport = true;
        }
    }
}