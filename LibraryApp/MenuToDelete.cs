using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
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
    }
}
