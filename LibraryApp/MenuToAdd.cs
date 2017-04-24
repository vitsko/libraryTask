namespace LibraryApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MenuToAdd
    {
        private static char[] comma = { ',' };

        internal static void Draw()
        {
            Console.Clear();
            Console.WriteLine(Display.AddMenu);
        }

        internal static string[] Input(ConsoleKeyInfo toAddIntoCatalog)
        {
            Console.Clear();
            List<string> questions = new List<string>();

            switch (toAddIntoCatalog.Key)
            {
                case ConsoleKey.D1:
                    {
                        questions = Display
                                    .AskBook
                                    .Split(comma, StringSplitOptions.RemoveEmptyEntries).ToList();
                        break;
                    }

                case ConsoleKey.D2:
                    {
                        questions = Display
                                    .AskNewspaper
                                    .Split(comma, StringSplitOptions.RemoveEmptyEntries).ToList();
                        break;
                    }

                case ConsoleKey.D3:
                    {
                        questions = Display
                                    .AskPatent
                                    .Split(comma, StringSplitOptions.RemoveEmptyEntries).ToList();
                        break;
                    }

                default: break;
            }

            return InputInfoAboutItemCatalog(questions);
        }

        private static string[] InputInfoAboutItemCatalog(List<string> questions)
        {
            List<string> infoAboutItemCatalog = new List<string>();

            foreach (var item in questions)
            {
                Console.WriteLine(item);
                var info = Console.ReadLine();
                infoAboutItemCatalog.Add(info);
            }

            return infoAboutItemCatalog.ToArray();
        }
    }
}
