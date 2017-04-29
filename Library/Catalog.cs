namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Catalog
    {
        private const int SizeArray = 1000;
        private static ItemCatalog[] libraryItem;

        static Catalog()
        {
            libraryItem = new ItemCatalog[SizeArray];
        }

        public static int Count
        {
            get
            {
                return Catalog.AllItemCatalog
                              .Count();
            }
        }

        public static bool HasBook
        {
            get
            {
                return Catalog.AllItemCatalog.Any(item => item is Book);
            }
        }

        private static ItemCatalog[] AllItemCatalog
        {
            get
            {
                return Catalog.Shift();
            }
        }

        public static void Add(ItemCatalog item)
        {
            var lastIndex = 0;

            if (Catalog.Count != 0)
            {
                lastIndex = Catalog.Count;
            }

            Catalog.libraryItem[lastIndex] = item;
        }

        public static bool Delete(int id)
        {
            if (Catalog.AllItemCatalog.Any(item => item.Id == id))
            {
                for (int index = 0; index < Catalog.Count; index++)
                {
                    if (Catalog.AllItemCatalog[index].Id == id)
                    {
                        Catalog.libraryItem[index] = null;

                        var allItem = Catalog.Shift();

                        Array.Clear(Catalog.libraryItem, 0, Catalog.libraryItem.Length);
                        Array.Copy(allItem, Catalog.libraryItem, allItem.Length);

                        return true;
                    }
                }
            }

            return false;
        }

        public static void DeleteAll()
        {
            for (int index = 0; index < Catalog.SizeArray; index++)
            {
                if (Catalog.libraryItem[index] != null)
                {
                    Catalog.libraryItem[index] = null;
                }
            }
        }

        public static ItemCatalog[] GetItemWithTitle(string title)
        {
            return Array.FindAll(Catalog.AllItemCatalog, item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        public static ItemCatalog[] SortByYearAsc()
        {
            return SortByYear();
        }

        public static ItemCatalog[] SortByYearDesc()
        {
            var allitem = SortByYear();
            Array.Reverse(allitem);
            return allitem;
        }

        public static ItemCatalog[] GetBookByAuthor(string authorForSearch)
        {
            IEqualityComparer<string> comparator = new ComparatorByContains();

            var books = Catalog.AllItemCatalog.Where(item => item is Book).ToArray();
            return Array.FindAll(books, book => ((Book)book).Authors.Contains(authorForSearch, comparator));
        }

        public static ItemCatalog[] GroupBooksByPublisher(string publisher)
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

                    return Catalog.GetItemByGrouped(result);
                }
            }

            return books;
        }

        public static ItemCatalog[] GroupByYear()
        {
            var result = from items in Catalog.AllItemCatalog
                         group items by items.PublishedYear;

            return Catalog.GetItemByGrouped(result);
        }

        public static string GetInfoCatalog()
        {
            return Catalog.GetInfoSelectedItem(Catalog.AllItemCatalog).ToString();
        }

        public static string GetInfoSelectedItem(ItemCatalog[] selectedItems)
        {
            StringBuilder aboutItems = new StringBuilder();

            foreach (var item in selectedItems)
            {
                aboutItems.AppendLine(item.ToString());
            }

            return aboutItems.ToString();
        }

        private static ItemCatalog[] SortByYear()
        {
            var allitem = Catalog.Shift();
            Array.Sort(allitem);
            return allitem;
        }

        private static ItemCatalog[] GetItemByGrouped(dynamic resultGroup)
        {
            var groupingItem = new List<ItemCatalog>();

            foreach (var group in resultGroup)
            {
                foreach (var item in group)
                {
                    groupingItem.Add(item);
                }
            }

            return groupingItem.ToArray();
        }

        private static ItemCatalog[] Shift()
        {
            return Catalog.libraryItem.Where(item => item != null).ToArray();
        }
    }
}