namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public sealed class Catalog
    {
        private const int SizeArray = 1000;
        private static readonly Catalog Instance = new Catalog();
        private static ItemCatalog[] libraryItem;

        private Catalog()
        {
            libraryItem = new ItemCatalog[SizeArray];
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
            var lastIndex = 0;

            if (Catalog.Count != 0)
            {
                lastIndex = Catalog.Count;
            }

            Catalog.libraryItem[lastIndex] = item;
        }

        public bool Delete(int index)
        {
            if (index >= 1 && Catalog.Count >= index)
            {
                Catalog.libraryItem[index - 1] = null;

                var allItem = Catalog.Shift();

                Array.Clear(Catalog.libraryItem, 0, Catalog.libraryItem.Length);
                Array.Copy(allItem, Catalog.libraryItem, allItem.Length);

                return true;
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
            return Catalog.GetInfoCatalog(allitem);
        }

        public string SortByYearDesc()
        {
            var allitem = Catalog.Shift();
            Array.Sort(allitem);
            Array.Reverse(allitem);
            return Catalog.GetInfoCatalog(allitem);
        }

        public string InfoBookByAuthor(string authorForSearch)
        {
            IEqualityComparer<string> comparator = new ComparatorByContains();

            var books = Catalog.AllItemCatalog.Where(item => item is Book == true).ToArray();
            var booksByAuthor = Array.FindAll(books, book => ((Book)book).Authors.Contains(authorForSearch, comparator));
            return Catalog.GetInfoCatalog(booksByAuthor);
        }

        public string GroupBooksByPublisher(string publisher)
        {
            string pattern = @"^" + publisher + "";
            var books = Catalog.AllItemCatalog.Where(item => item is Book == true).ToArray();

            if (books.Length != 0)
            {
                var booksWithResult = new List<Book>();

                foreach (var book in books)
                {
                    var onlyBook = (Book)book;

                    if (Regex.Match(onlyBook.Publisher, pattern, RegexOptions.IgnoreCase).Success)
                    {
                        booksWithResult.Add(onlyBook);
                    }
                }

                if (booksWithResult.Count != 0)
                {
                    var result = from book in booksWithResult
                                 group book by book.Publisher;

                    return Catalog.GetInfoByGroupedItem(result);
                }
            }

            return string.Empty;
        }

        public string GroupByYear()
        {
            var result = from items in Catalog.AllItemCatalog
                         group items by items.PublishedYear;

            return Catalog.GetInfoByGroupedItem(result);
        }

        private static string GetInfoByGroupedItem(dynamic resultGroup)
        {
            var groupingItem = new List<ItemCatalog>();

            foreach (var group in resultGroup)
            {
                foreach (var item in group)
                {
                    groupingItem.Add(item);
                }
            }

            return Catalog.GetInfoCatalog(groupingItem.ToArray());
        }

        private static string GetInfoCatalog(ItemCatalog[] allitem)
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

        private static ItemCatalog[] Shift()
        {
            return Catalog.libraryItem.Where(item => item != null).ToArray();
        }
    }
}
