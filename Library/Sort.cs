namespace Library
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Sorter
    {
        public static Sorting SortByYearASC = (List<ItemCatalog> catalog) =>
        {
            var result = catalog.ToList();
            result.Sort();
            return result;
        };

        public static Sorting SortByYearDESC = (List<ItemCatalog> catalog) =>
        {
            var catalogToSort = SortByYearASC(catalog);
            catalogToSort.Reverse();
            return catalogToSort;
        };

        public static Sorting GroupByYear = (List<ItemCatalog> catalog) =>
        {
            return from items in catalog
                   group items by items.PublishedYear;
        };

        public delegate dynamic Sorting(List<ItemCatalog> catalog);

        public static dynamic Sort(Sorting sort, List<ItemCatalog> catalog)
        {
            return sort(catalog);
        }
    }
}