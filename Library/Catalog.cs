namespace Library
{
    using Helper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using Resource;

    public static class Catalog
    {
        private static List<ItemCatalog> libraryItem;

        static Catalog()
        {
            libraryItem = new List<ItemCatalog>();
        }

        public enum GroupingBy
        {
            Publisher = 1,
            Year
        }

        public static List<ItemCatalog> AllItem
        {
            get
            {
                return Catalog.libraryItem;
            }
        }

        public static int Count
        {
            get
            {
                return Catalog.libraryItem
                              .Count();
            }
        }

        public static bool HasBook
        {
            get
            {
                return Catalog.libraryItem.Any(item => item is Book);
            }
        }

        public static bool Add(ItemCatalog item)
        {
            Catalog.libraryItem.Add(item);
            return ItemCatalog.IsCorrectCreating();
        }

        public static bool Delete(int id)
        {
            if (Catalog.libraryItem.Any(item => item.Id == id))
            {
                for (int index = 0; index < Catalog.Count; index++)
                {
                    if (Catalog.libraryItem[index].Id == id)
                    {
                        Catalog.libraryItem.RemoveAt(index);

                        return true;
                    }
                }
            }

            return false;
        }

        public static void DeleteAll()
        {
            for (int index = 0; index < Catalog.Count; index++)
            {
                if (Catalog.libraryItem[index] != null)
                {
                    Catalog.libraryItem[index] = null;
                }
            }
        }

        public static bool Edit(ItemCatalog itemForEdit, List<string> aboutItemCatalog)
        {
            itemForEdit.Create(aboutItemCatalog);
            return ItemCatalog.IsCorrectCreating();
        }

        public static List<ItemCatalog> GetItemWithTitle(string title)
        {
            return Catalog.libraryItem.FindAll(item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        public static List<ItemCatalog> SortByYearASC()
        {
            var catalogToSort = Catalog.libraryItem.ToList();
            catalogToSort.Sort();

            return catalogToSort;
        }

        public static List<ItemCatalog> SortByYearDESC()
        {
            var catalogToSort = SortByYearASC();
            catalogToSort.Reverse();
            return catalogToSort;
        }

        public static List<ItemCatalog> GetBookByAuthor(string authorForSearch)
        {
            IEqualityComparer<string> comparator = new Comparator();
            var books = Catalog.libraryItem.Where(item => item is Book).ToList();

            return books.FindAll(book => ((Book)book).Authors.Contains(authorForSearch, comparator));
        }

        public static dynamic GroupBooksByPublisher(string publisher)
        {
            var books = Catalog.libraryItem.Where(item => item is Book == true).ToList();
            IEnumerable<IGrouping<string, Book>> emptyResult = null;

            if (books.Count != 0)
            {
                var booksWithResult = new List<Book>();

                foreach (var book in books)
                {
                    var onlyBook = (Book)book;

                    if (string.Compare(onlyBook.Publisher, 0, publisher, 0, publisher.Length, StringComparison.OrdinalIgnoreCase) == 0)
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
            return from items in Catalog.libraryItem
                   group items by items.PublishedYear;
        }

        public static string GetInfoCatalog()
        {
            return Catalog.GetInfoSelectedItem(Catalog.libraryItem).ToString();
        }

        public static string GetInfoSelectedItem(List<ItemCatalog> selectedItems)
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

                    aboutGroupedItems.AppendLine(string.Format(Titles.AboutGrouping, key));
                    aboutGroupedItems.AppendLine();

                    foreach (ItemCatalog item in group)
                    {
                        aboutGroupedItems.AppendLine(item.ToString());
                    }
                }
            }
            else
            {
                aboutGroupedItems.AppendLine(Titles.GroupingIsUnavailable);
            }

            return aboutGroupedItems.ToString();
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

        private static int GetIndexByID(int id)
        {
            int index = 0;

            foreach (var item in Catalog.libraryItem)
            {
                if (item.Id != id)
                {
                    index++;
                }
                else
                {
                    break;
                }
            }

            return index;
        }
    }
}