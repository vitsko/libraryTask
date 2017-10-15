namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helper;

    public static class Searcher
    {
        public static Searching GetItemWithTitle = (string title, List<ItemCatalog> catalog) =>
        {
            return

            (from item in catalog
             where item.Title.Contains(title, StringComparison.OrdinalIgnoreCase)
             select item).ToList();
        };

        public static Searching GetBookByAuthor = (string authorForSearch, List<ItemCatalog> catalog) =>
        {
            IEqualityComparer<string> comparator = new Comparator();

            return

            (from book in catalog
             where ((Book)book).Authors.Contains(authorForSearch, comparator)
             select book).ToList();
        };

        public static Searching GroupBooksByPublisher = (string publisher, List<ItemCatalog> catalog) =>
        {
            IEnumerable<IGrouping<string, Book>> emptyResult = null;

            if (catalog.Count != 0)
            {
                var booksWithResult = new List<Book>();

                foreach (var book in catalog)
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

        public delegate dynamic Searching(string criterionSerch, List<ItemCatalog> catalog);

        public static dynamic Search(Searching searcher, string toSearch, List<ItemCatalog> catalog)
        {
            return searcher(toSearch, catalog);
        }
    }
}