namespace LibraryApp
{
    using Helper;
    using Library;
    using Resource;
    using System;
    using System.Collections.Generic;

    internal static class Screen
    {
        private static char[] comma = { ',' };

        internal static void ShowResultAddOrEdit(bool isCorrectAdd, ItemCatalog item)
        {
            Console.Clear();

            if (!isCorrectAdd)
            {
                Console.WriteLine(Titles.ListError);
                foreach (var error in ItemCatalog.GetListOfError())
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
                    {
                        questions = Helper.GetyQuestions(Titles.AskAboutBook); ;
                        break;
                    }

                case ConsoleKey.D2:
                    {
                        questions = Helper.GetyQuestions(Titles.AskAboutNewspaper);
                        break;
                    }

                case ConsoleKey.D3:
                    {
                        questions = Helper.GetyQuestions(Titles.AskAboutPatent);
                        break;
                    }

                default: break;
            }

            return InputInfoAboutItemCatalog(questions);
        }

        public static List<string> InputInfoAboutItemCatalog(List<string> questions)
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
    }
}