namespace Library
{
    using System;
    using System.Linq;
    using System.Text;

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

        public static int Count
        {
            get
            {
                return Catalog.AllItemCatalog
                              .Count();
            }
        }

        private static ItemCatalog[] AllItemCatalog
        {
            get
            {
                return Catalog.Shift();
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

        public string GetInfoCatalog()
        {
            return Catalog.GetInfoSelectedItem(Catalog.AllItemCatalog).ToString();
        }

        public string GetInfoItemWithTitle(string title)
        {
            var items = Array.FindAll(Catalog.AllItemCatalog, item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

            return GetInfoSelectedItem(items).ToString();
        }

        public string SortByYearAsc()
        {
            var allitem = Catalog.Shift();
            Array.Sort(allitem);
            return this.GetInfoCatalog(allitem);
        }

        public string SortByYearDesc()
        {
            var allitem = Catalog.Shift();
            Array.Sort(allitem);
            Array.Reverse(allitem);
            return this.GetInfoCatalog(allitem);
        }

        private string GetInfoCatalog(ItemCatalog[] allitem)
        {
            return Catalog.GetInfoSelectedItem(allitem).ToString();
        }

        private static StringBuilder GetInfoSelectedItem(ItemCatalog[] selectedItems)
        {
            StringBuilder aboutItems = new StringBuilder();

            foreach (var item in selectedItems)
            {
                aboutItems.AppendLine(item.ToString());
            }

            return aboutItems;
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
