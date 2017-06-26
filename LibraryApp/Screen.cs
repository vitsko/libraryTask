namespace LibraryApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Helper;
    using Library;
    using Resource;

    internal static class Screen
    {
        private static char[] comma = { ',' };

        internal static void ShowResultAddOrEdit(ItemCatalog item)
        {
            Console.Clear();

            if (!item.IsCorrectCreating())
            {
                Console.WriteLine(Titles.ListError);
                foreach (var error in item.ErrorList)
                {
                    Console.WriteLine(error);
                }

                Console.WriteLine();
            }

            Console.WriteLine(Titles.ResultAddorEdit);
            Screen.ShowText(item.ToString(), Titles.PressAnyKey);
        }

        internal static void ShowText(params string[] textToShow)
        {
            foreach (var stringToShow in textToShow)
            {
                Console.WriteLine(stringToShow);
            }
        }

        internal static List<string> Input(ConsoleKey toAddIntoCatalog)
        {
            Console.Clear();
            List<string> questions = new List<string>();

            switch (toAddIntoCatalog)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    {
                        questions = Helper.GetyQuestions(Titles.AskAboutBook);
                        break;
                    }

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    {
                        questions = Helper.GetyQuestions(Titles.AskAboutNewspaper);
                        break;
                    }

                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    {
                        questions = Helper.GetyQuestions(Titles.AskAboutPatent);
                        break;
                    }

                default: break;
            }

            return InputInfoAboutItemCatalog(questions);
        }

        internal static List<string> InputInfoAboutItemCatalog(List<string> questions)
        {
            List<string> infoAboutItemCatalog = new List<string>();

            foreach (var item in questions)
            {
                Console.WriteLine(item);
                var info = Console.ReadLine();
                infoAboutItemCatalog.Add(info);
            }

            return infoAboutItemCatalog;
        }

        internal static string ResultImportToLog()
        {
            var result = new StringBuilder();

            foreach (var item in Catalog.AllItem)
            {
                if (!item.IsCorrectCreating())
                {
                    result.AppendFormat(Titles.AboutErrorToImport, item.Id.ToString(), item.TypeItem);
                    result.AppendLine();

                    foreach (var error in item.ErrorList)
                    {
                        result.AppendLine(error);
                    }

                    result.AppendLine();
                }
                else
                {
                    result.AppendFormat(Titles.AboutCorrectItem, item.Id.ToString(), item.TypeItem);
                    result.AppendLine();
                }
            }

            return result.ToString();
        }
    }
}