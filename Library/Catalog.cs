namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Catalog
    {
        public enum GroupingBy
        {
            Publisher = 1,
            Year
        }

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

        public static ItemCatalog[] SortByYearASC()
        {
            var allitem = Catalog.Shift();
            Array.Sort(allitem);
            return allitem;
        }

        public static ItemCatalog[] SortByYearDESC()
        {
            var allitem = SortByYearASC();
            Array.Reverse(allitem);
            return allitem;
        }

        public static ItemCatalog[] GetBookByAuthor(string authorForSearch)
        {
            IEqualityComparer<string> comparator = new ComparatorByContains();

            var books = Catalog.AllItemCatalog.Where(item => item is Book).ToArray();
            return Array.FindAll(books, book => ((Book)book).Authors.Contains(authorForSearch, comparator));
        }

        public static dynamic GroupBooksByPublisher(string publisher)
        {
            string pattern = @"^" + publisher + "";
            var books = Catalog.AllItemCatalog.Where(item => item is Book == true).ToArray();
            IEnumerable<IGrouping<string, Book>> emptyResult = null;

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
                    return from book in booksWithResult
                           group book by book.Publisher;
                }
            }

            return emptyResult;
        }

        public static dynamic GroupByYear()
        {
            return from items in Catalog.AllItemCatalog
                   group items by items.PublishedYear;
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

        public static string GetInfoItemByGrouping(dynamic resultGroup, GroupingBy propertyToGrouping)
        {
            StringBuilder aboutGroupedItems = new StringBuilder();

            if (resultGroup != null)
            {
                foreach (var group in resultGroup)
                {
                    var key = Catalog.GetKeyForgrouping(group, propertyToGrouping);

                    aboutGroupedItems.AppendLine(AboutObject.AboutGrouping + @"'" + key + @"' :");
                    aboutGroupedItems.AppendLine();

                    foreach (ItemCatalog item in group)
                    {
                        aboutGroupedItems.AppendLine(item.ToString());
                    }
                }
            }
            else
            {
                aboutGroupedItems.AppendLine(AboutObject.GroupingIsUnavailable);
            }

            return aboutGroupedItems.ToString();
        }

        private static ItemCatalog[] Shift()
        {
            return Catalog.libraryItem.Where(item => item != null).ToArray();
        }

        private static string GetKeyForgrouping(dynamic group, GroupingBy propertyToGrouping)
        {
            var key = string.Empty;

            if (propertyToGrouping == GroupingBy.Publisher)
            {
                key = ((IGrouping<string, Book>)group).Key.ToString();
            }
            else
            {
                key = ((IGrouping<int, ItemCatalog>)group).Key.ToString();
            }

            return key;
        }
    }
}