namespace LibraryApp
{
    using System;

    internal static class MenuToDelete
    {
        internal static void Draw()
        {
            Console.Clear();
            Console.WriteLine(Display.AskToDelete);
        }

        internal static void AboutDelete(string indexToDelete)
        {
            Console.Clear();
            Console.WriteLine(Display.Delete, indexToDelete);
            Console.ReadKey();
        }

        internal static void NotDelete(string indexToDelete)
        {
            Console.Clear();
            Console.WriteLine(Display.NoDelete, indexToDelete);
            Console.ReadKey();
        }

        internal static void AboutEmptyCatalog()
        {
            Console.Clear();
            Console.WriteLine(Display.EmptyCatalog);
            Console.ReadKey();
        }
    }
}
