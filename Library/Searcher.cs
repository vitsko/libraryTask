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
    return catalog.FindAll(item => item.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
};

        public static Searching GetBookByAuthor = (string authorForSearch, List<ItemCatalog> catalog) =>
        {
            IEqualityComparer<string> comparator = new Comparator();

            return catalog.FindAll(book => ((Book)book).Authors.Contains(authorForSearch, comparator));
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