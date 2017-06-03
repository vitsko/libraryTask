namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Helper;
    using Resource;

    public static class Catalog
    {
        public static Searcher GetItemWithTitle = title =>
        {
            return Catalog.libraryItem.FindAll(item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        };

        public static Sorter SortByYearASC = () =>
        {
            var result = Catalog.libraryItem.ToList();
            result.Sort();
            return result;
        };

        public static Sorter SortByYearDESC = () =>
        {
            var catalogToSort = SortByYearASC();
            catalogToSort.Reverse();
            return catalogToSort;
        };

        public static Searcher GetBookByAuthor = authorForSearch =>
        {
            IEqualityComparer<string> comparator = new Comparator();

            return Catalog.OnlyBooks.FindAll(book => ((Book)book).Authors.Contains(authorForSearch, comparator));
        };

        public static Searcher GroupBooksByPublisher = publisher =>
        {
            IEnumerable<IGrouping<string, Book>> emptyResult = null;

            if (Catalog.OnlyBooks.Count != 0)
            {
                var booksWithResult = new List<Book>();

                foreach (var book in Catalog.OnlyBooks)
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
        };

        public static Sorter GroupByYear = () =>
        {
            return from items in Catalog.libraryItem
                   group items by items.PublishedYear;
        };

        private static List<ItemCatalog> libraryItem;

        static Catalog()
        {
            libraryItem = new List<ItemCatalog>();
        }

        public delegate dynamic Searcher(string criterionSerch);

        public delegate dynamic Sorter();

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

        public static bool IsNotEmpty
        {
            get
            {
                return Catalog.Count != 0;
            }
        }

        private static List<ItemCatalog> OnlyBooks
        {
            get
            {
                return Catalog.libraryItem.Where(item => item is Book).ToList();
            }
        }

        public static void Add(ItemCatalog item)
        {
            Catalog.libraryItem.Add(item);
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
            Catalog.libraryItem.Clear();
        }

        public static void Edit(ItemCatalog itemForEdit, List<string> aboutItemCatalog)
        {
            itemForEdit.Create(aboutItemCatalog);
        }

        public static dynamic Search(Searcher searcher, string toSearch)
        {
            return searcher(toSearch);
        }

        public static dynamic Sort(Sorter sort)
        {
            return sort();
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

        public static string Save()
        {
            StringBuilder lines = new StringBuilder();

            foreach (var item in Catalog.libraryItem)
            {
                lines.AppendLine(item.GetInfoToSave());
            }

            return lines.ToString();
        }

        public static bool LoadWithoutError(List<List<string>> stringsFromFile)
        {
            List<ItemCatalog> tempCatalog = null;

            foreach (var item in stringsFromFile)
            {
                var toCatalog = ItemCatalog.CreateFromFile(item);

                if (toCatalog.IsCorrectCreating())
                {
                    if (tempCatalog == null)
                    {
                        tempCatalog = new List<ItemCatalog>();
                    }

                    tempCatalog.Add(toCatalog);
                }
                else
                {
                    return false;
                }
            }

            Catalog.libraryItem.Clear();
            Catalog.libraryItem = new List<ItemCatalog>(tempCatalog);

            return true;
        }

        public static void Load(List<List<string>> stringsFromFile)
        {
            List<ItemCatalog> tempCatalog = null;

            foreach (var item in stringsFromFile)
            {
                var toCatalog = ItemCatalog.CreateFromFile(item);

                if (tempCatalog == null)
                {
                    tempCatalog = new List<ItemCatalog>();
                }

                tempCatalog.Add(toCatalog);
            }

            Catalog.libraryItem.Clear();
            Catalog.libraryItem = new List<ItemCatalog>(tempCatalog);
        }

        private static string GetKeyForgrouping(dynamic group, GroupingBy propertyToGrouping)
        {
            return propertyToGrouping == GroupingBy.Publisher ?
                   ((IGrouping<string, Book>)group).Key :
                   ((IGrouping<int, ItemCatalog>)group).Key.ToString();
        }
    }
}