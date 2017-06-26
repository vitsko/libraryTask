namespace LibraryApp
{
    using System;
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
                    case ConsoleKey.NumPad1:
                        {
                            MainMenu.Add();
                            break;
                        }

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        {
                            MainMenu.Edit();
                            break;
                        }

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        {
                            MainMenu.Delete();
                            break;
                        }

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        {
                            MainMenu.ShowCatalog();
                            break;
                        }

                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        {
                            MainMenu.SearchByTitle();
                            break;
                        }

                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        {
                            MainMenu.SortByYear();
                            break;
                        }

                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        {
                            MainMenu.SearchBooksByAuthor();
                            break;
                        }

                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        {
                            MainMenu.SearchBooksByPublisher();
                            break;
                        }

                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        {
                            MainMenu.GroupByYear();
                            break;
                        }

                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        {
                            MainMenu.WorkWithStorage();
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
