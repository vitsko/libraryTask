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

            // Delete after testing.
           // libraryItem[0] = new ItemCatalog();
           // libraryItem[2] = new ItemCatalog();
           // libraryItem[5] = new ItemCatalog();
        }

        public static Catalog Library
        {
            get
            {
                return Instance;
            }
        }

        public void Add(ItemCatalog item)
        {
            this.AddToHead();
            this.AddToEnd(item);
        }

        private void AddToHead()
        {
            var allItems = this.Shift();

            for (int index = 0; index < Catalog.libraryItem.Length; index++)
            {
                Catalog.libraryItem[index] = null;
            }

            Array.Copy(allItems, Catalog.libraryItem, allItems.Length);
        }

        private ItemCatalog[] Shift()
        {
            return libraryItem.Where(item => item != null).ToArray();
        }

        private void AddToEnd(ItemCatalog item)
        {
            var lastIndex = Catalog.libraryItem.ToList().FindIndex(x => x == null);
            Catalog.libraryItem[lastIndex] = item;
        }
    }
}
