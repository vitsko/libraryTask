using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test
{
    using Library;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TestLibrary
    {
        private const int COUNTALL = 6;
        private const int AFTERDELETE = 4;
        private const string searchTitle = "Советский";
        private const int idByTitleSearch = 3;
        private const int idBySortYearASC = 1;
        private const int idBySortYearDESC = 3;
        private const string searchByAuthor = "Михаил";
        private const int countBookByAuthor = 2;


        [TestMethod]
        public void CheckAdd()
        {
            TestLibrary.GetDataToTest();
            Assert.IsTrue(Catalog.Count == TestLibrary.COUNTALL);
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

            Assert.IsTrue(Catalog.Count == TestLibrary.AFTERDELETE);
        }

        [TestMethod]
        public void CheckSearchByTitle()
        {
            TestLibrary.GetDataToTest();
            var result = Catalog.GetItemWithTitle(searchTitle);

            Assert.IsTrue(result[0].Id == idByTitleSearch);
        }

        [TestMethod]
        public void CheckSortByYearASC()
        {
            TestLibrary.GetDataToTest();
            var result = Catalog.SortByYearASC();

            Assert.IsTrue(result[0].Id == TestLibrary.idBySortYearASC);
        }

        [TestMethod]
        public void CheckSortByYearDESC()
        {
            TestLibrary.GetDataToTest();
            var result = Catalog.SortByYearDESC();

            Assert.IsTrue(result[0].Id == TestLibrary.idBySortYearDESC);
        }

        [TestMethod]
        public void CheckSearchBookByAuthor()
        {
            var book = Catalog.GetBookByAuthor(TestLibrary.searchByAuthor);
            Assert.IsTrue(book.Length == TestLibrary.countBookByAuthor);
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
