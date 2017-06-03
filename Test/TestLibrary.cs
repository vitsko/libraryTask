namespace Test
{
    using System.Linq;
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
        private const int CountLoadedItem = 1;

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

            Catalog.Delete(1);
            Catalog.Delete(-1);
            Catalog.Delete(1);
            Catalog.Delete(7);
            Catalog.Delete(6);

            Assert.IsTrue(Catalog.Count == TestLibrary.LengthTestCatalogAfterDelete);
        }

        [TestMethod]
        public void CheckEdit()
        {
            TestLibrary.GetDataToTest();

            var newData = DataToTest.InfoToCatalog().ElementAt(6);

            var toEdit = Catalog.AllItem.ElementAt(0);
            var oldTitle = toEdit.Title;

            Catalog.Edit(toEdit, newData);

            Assert.IsTrue(!toEdit.Title.Equals(oldTitle));
        }

        [TestMethod]
        public void CheckValidate()
        {
            TestLibrary.GetDataToTest();

            Assert.IsTrue(Catalog.AllItem.ElementAt(0).IsCorrectCreating() == false);
        }

        [TestMethod]
        public void CheckSearchByTitle()
        {
            TestLibrary.GetDataToTest();

            var seacher = Catalog.GetItemWithTitle;
            var result = Catalog.Search(seacher, SearchTitle);

            Assert.IsTrue(result[0].Id == IdByTitleSearch);
        }

        [TestMethod]
        public void CheckSortByYearASC()
        {
            TestLibrary.GetDataToTest();

            var sortByYear = Catalog.SortByYearASC;
            var result = Catalog.Sort(sortByYear);

            Assert.IsTrue(result[0].Id == TestLibrary.IdBySortYearASC);
        }

        [TestMethod]
        public void CheckSortByYearDESC()
        {
            TestLibrary.GetDataToTest();

            var sortByYear = Catalog.SortByYearDESC;
            var result = Catalog.Sort(sortByYear);

            Assert.IsTrue(result[0].Id == TestLibrary.IdBySortYearDESC);
        }

        [TestMethod]
        public void CheckSearchBookByAuthor()
        {
            TestLibrary.GetDataToTest();

            var seacher = Catalog.GetBookByAuthor;
            var booksbyAuthor = Catalog.Search(seacher, TestLibrary.SearchByAuthor);

            Assert.IsTrue(booksbyAuthor.Count == TestLibrary.CountBookByAuthor);
        }

        [TestMethod]
        public void CheckGroupingBookByPublisher()
        {
            TestLibrary.GetDataToTest();

            var seacher = Catalog.GroupBooksByPublisher;
            var books = Catalog.Search(seacher, PublisherForGrouping);

            Assert.IsTrue(TestLibrary.CountOfGroup(books) == TestLibrary.CountOfGroupForPublisher);
        }

        [TestMethod]
        public void CheckGroupingItemByYear()
        {
            TestLibrary.GetDataToTest();

            var groupedCatalogByYear = Catalog.GroupByYear;
            var resultGrouping = Catalog.Sort(groupedCatalogByYear);

            Assert.IsTrue(TestLibrary.CountOfGroup(resultGrouping) == TestLibrary.CountOfGroupForYear);
        }

        [TestMethod]
        public void CheckSave()
        {
            Catalog.Add(new Book(DataToTest.InfoToCatalog().ElementAt(3)));

            string toSave = Catalog.Save();
            string toCompare = DataToTest.ToCompareForSave;

            Assert.IsTrue(toSave.Equals(toCompare));
        }

        [TestMethod]
        public void CheckLoadWithoutError()
        {
            TestLibrary.GetDataToTest();

            var aboutItemsToLoad = DataToTest.ToLoad;
            var isLoad = Catalog.LoadWithoutError(aboutItemsToLoad);

            Assert.IsTrue(isLoad);
        }

        [TestMethod]
        public void CheckWithoutLoad()
        {
            TestLibrary.GetDataToTest();

            var aboutItemsToLoad = DataToTest.IncorrectToLoad;
            var isLoad = Catalog.LoadWithoutError(aboutItemsToLoad);

            Assert.IsTrue(!isLoad);
        }

        [TestMethod]
        public void CheckLoad()
        {
            TestLibrary.GetDataToTest();

            var aboutItemsToLoad = DataToTest.IncorrectToLoad;
            Catalog.Load(aboutItemsToLoad);

            Assert.IsTrue(Catalog.Count == TestLibrary.CountLoadedItem);
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