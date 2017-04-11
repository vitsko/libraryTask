namespace Library
{
    using System;
    using System.Linq;

    public sealed class Catalog
    {
        private static readonly Catalog Instance = new Catalog();
        private static ItemCatalog[] libraryItem;

        private Catalog()
        {
            libraryItem = new ItemCatalog[100];
        }

        public static Catalog Library
        {
            get
            {
                return Instance;
            }
        }

        private static int Count
        {
            get
            {
                return Catalog.Shift()
                              .Count();
            }
        }

        public void Add(ItemCatalog item)
        {
            Catalog.AddToHead();
            Catalog.AddToEnd(item);
        }

        public bool Delete(string indexItem)
        {
            int index = -1;
            bool isInt = int.TryParse(indexItem, out index);

            if (isInt == true && index >= 0)
            {
                if (Catalog.Count >= index + 1)
                {
                    Catalog.libraryItem[index - 1] = null;
                    Catalog.AddToHead();
                    return true;
                }
            }

            return false;
        }

        private static void AddToHead()
        {
            var allItem = Catalog.Shift();

            Array.Clear(Catalog.libraryItem, 0, Catalog.libraryItem.Length);
            Array.Copy(allItem, Catalog.libraryItem, allItem.Length);
        }

        private static ItemCatalog[] Shift()
        {
            return Catalog.libraryItem.Where(item => item != null).ToArray();
        }

        private static void AddToEnd(ItemCatalog item)
        {
            var lastIndex = Catalog.libraryItem.ToList().FindIndex(x => x == null);
            Catalog.libraryItem[lastIndex] = item;
        }
    }
}
