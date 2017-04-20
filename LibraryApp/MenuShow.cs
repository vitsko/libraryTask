namespace LibraryApp
{
    using System;

    internal static class MenuShow
    {
        internal static void AboutCatalog(string infoCatalog)
        {
            Console.Clear();
            Console.WriteLine(Display.ShowCatalog);
            MenuShow.ShowResult(infoCatalog);
        }

        internal static void ResultSearchOfTitle(string titleSearch, string result)
        {
            Console.Clear();
            Console.WriteLine(Display.ResultSearchOfTitle, titleSearch);
            MenuShow.ShowResult(result);
        }

        internal static void ResultSearchByAuthors(string authorSearch, string result)
        {
            Console.Clear();
            Console.WriteLine(Display.ResultSearchOfAuthors, authorSearch);
            MenuShow.ShowResult(result);
        }

        internal static void ResultGroupByPublisher(string searchByPublisher, string result)
        {
            Console.Clear();
            Console.WriteLine(Display.ResultGroupByPublisher, searchByPublisher);
            MenuShow.ShowResult(result);
        }

        internal static void InputSeachRequest()
        {
            Console.Clear();
            Console.WriteLine(Display.InputSeachRequest);
        }

        private static void ShowResult(string result)
        {
            Console.WriteLine(result);
            Console.WriteLine(Display.PressAnyKey);
            Console.ReadKey();
        }
    }
}
