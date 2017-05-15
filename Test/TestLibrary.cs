namespace Test
{
    using Library;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestLibrary
    {
        private const int LengthTestCatalog = 6;
        private const int LengthTestCatalogAfterDelete = 4;
        private const string SearchTitle = "Советский";
        private const int IdByTitleSearch = 3;
        private const int IdBySortYearASC = 1;
        private const int IdBySortYearDESC = 3;
        private const string SearchByAuthor = "Михаил";
        private const int CountBookByAuthor = 2;
        private const string PublisherForGrouping = "Моск";
        private const int CountOfGroupForPublisher = 2;
        private const int CountOfGroupForYear = 4;

        [TestMethod]
        public void CheckAdd()
        {
            TestLibrary.GetDataToTest();
            Assert.IsTrue(Catalog.Count == TestLibrary.LengthTestCatalog);
        }

        [TestMethod]
        public void CheckDelete()
        {
            TestLibrary.GetDataToTest();
            Catalog.Delete(0);
            Catalog.Delete(-1);
            Catalog.Delete(1);
            Catalog.Delete(7);
            Catalog.Delete(6);

            Assert.IsTrue(Catalog.Count == TestLibrary.LengthTestCatalogAfterDelete);
        }

        [TestMethod]
        public void CheckSearchByTitle()
        {
            TestLibrary.GetDataToTest();
            var result = Catalog.GetItemWithTitle(SearchTitle);

            Assert.IsTrue(result[0].Id == IdByTitleSearch);
        }

        [TestMethod]
        public void CheckSortByYearASC()
        {
            TestLibrary.GetDataToTest();
            var result = Catalog.SortByYearASC();

            Assert.IsTrue(result[0].Id == TestLibrary.IdBySortYearASC);
        }

        [TestMethod]
        public void CheckSortByYearDESC()
        {
            TestLibrary.GetDataToTest();
            var result = Catalog.SortByYearDESC();

            Assert.IsTrue(result[0].Id == TestLibrary.IdBySortYearDESC);
        }

        [TestMethod]
        public void CheckSearchBookByAuthor()
        {
            TestLibrary.GetDataToTest();
            var book = Catalog.GetBookByAuthor(TestLibrary.SearchByAuthor);
            Assert.IsTrue(book.Count == TestLibrary.CountBookByAuthor);
        }

        [TestMethod]
        public void CheckGroupingBookByPublisher()
        {
            TestLibrary.GetDataToTest();

            var bookWithGrouping = Catalog.GroupBooksByPublisher(PublisherForGrouping);
            Assert.IsTrue(TestLibrary.CountOfGroup(bookWithGrouping) == TestLibrary.CountOfGroupForPublisher);
        }

        [TestMethod]
        public void CheckGroupingItemByYear()
        {
            TestLibrary.GetDataToTest();

            var catalogWithGrouping = Catalog.GroupByYear();
            Assert.IsTrue(TestLibrary.CountOfGroup(catalogWithGrouping) == TestLibrary.CountOfGroupForYear);
        }

        private static int CountOfGroup(dynamic resultOfGrouping)
        {
            int count = 0;

            foreach (var group in resultOfGrouping)
            {
                count++;
            }

            return count;
        }

        private static void GetDataToTest()
        {
            Catalog.DeleteAll();

            var allInfoToCatalog = DataToTest.InfoToCatalog();

            Catalog.Add(new Book(allInfoToCatalog[0]));
            Catalog.Add(new Book(allInfoToCatalog[3]));
            Catalog.Add(new Newspaper(allInfoToCatalog[1]));
            Catalog.Add(new Newspaper(allInfoToCatalog[4]));
            Catalog.Add(new Patent(allInfoToCatalog[2]));
            Catalog.Add(new Patent(allInfoToCatalog[5]));
        }
    }
}