﻿namespace LibraryApp
{
    using System;

    internal static class MenuShow
    {
        internal static void AboutCatalog(string infoCatalog)
        {
            Console.Clear();
            Console.WriteLine(Display.ShowCatalog);
            Console.WriteLine(infoCatalog);
            Console.WriteLine(Display.PressAnyKey);
            Console.ReadKey();
        }

        internal static void ResultSearchOfTitle(string titleSearch, string result)
        {
            Console.Clear();
            Console.WriteLine(Display.ResultSearchOfTitle, titleSearch);
            Console.WriteLine(result);
            Console.WriteLine(Display.PressAnyKey);
            Console.ReadKey();
        }

        internal static void InputSeachRequest()
        {
            Console.Clear();
            Console.WriteLine(Display.InputSeachRequest);
        }
    }
}
