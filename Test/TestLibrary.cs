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
            DataToTest.GetDataToTest();
            Assert.IsTrue(Catalog.Count == TestLibrary.LengthTestCatalog);
        }

        [TestMethod]
        public void CheckDelete()
        {
            DataToTest.GetDataToTest();

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
            DataToTest.GetDataToTest();

            var newData = DataToTest.InfoToCatalog().ElementAt(6);

            var toEdit = Catalog.AllItem.ElementAt(0);
            var oldTitle = toEdit.Title;

            Catalog.Edit(toEdit, newData);

            Assert.IsTrue(!toEdit.Title.Equals(oldTitle));
        }

        [TestMethod]
        public void CheckValidate()
        {
            DataToTest.GetDataToTest();

            Assert.IsTrue(Catalog.AllItem.ElementAt(0).IsCorrectCreating() == false);
        }

        [TestMethod]
        public void CheckSearchByTitle()
        {
            var seacher = Searcher.GetItemWithTitle;
            var result = Searcher.Search(seacher, SearchTitle, DataToTest.GetDataToSearchOrSort());

            Assert.IsTrue(result[0].Id == IdByTitleSearch);
        }

        [TestMethod]
        public void CheckSortByYearASC()
        {
            var sortByYear = Sorter.SortByYearASC;
            var result = Sorter.Sort(sortByYear, DataToTest.GetDataToSearchOrSort());

            Assert.IsTrue(result[0].Id == TestLibrary.IdBySortYearASC);
        }

        [TestMethod]
        public void CheckSortByYearDESC()
        {
            var sortByYear = Sorter.SortByYearDESC;
            var result = Sorter.Sort(sortByYear, DataToTest.GetDataToSearchOrSort());

            Assert.IsTrue(result[0].Id == TestLibrary.IdBySortYearDESC);
        }

        [TestMethod]
        public void CheckSearchBookByAuthor()
        {
            var seacher = Searcher.GetBookByAuthor;
            var booksbyAuthor = Searcher.Search(seacher, TestLibrary.SearchByAuthor, DataToTest.OnlyBook());

            Assert.IsTrue(booksbyAuthor.Count == TestLibrary.CountBookByAuthor);
        }

        [TestMethod]
        public void CheckGroupingBookByPublisher()
        {
            var seacher = Searcher.GroupBooksByPublisher;
            var books = Searcher.Search(seacher, PublisherForGrouping, DataToTest.OnlyBook());

            Assert.IsTrue(TestLibrary.CountOfGroup(books) == TestLibrary.CountOfGroupForPublisher);
        }

        [TestMethod]
        public void CheckGroupingItemByYear()
        {
            var groupedCatalogByYear = Sorter.GroupByYear;
            var resultGrouping = Sorter.Sort(groupedCatalogByYear, DataToTest.GetDataToSearchOrSort());

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
            var aboutItemsToLoad = DataToTest.ToLoad;
            var isLoad = Catalog.LoadWithoutError(aboutItemsToLoad);

            Assert.IsTrue(isLoad);
        }

        [TestMethod]
        public void CheckWithoutLoad()
        {
            var aboutItemsToLoad = DataToTest.IncorrectToLoad;
            var isLoad = Catalog.LoadWithoutError(aboutItemsToLoad);

            Assert.IsTrue(!isLoad);
        }

        [TestMethod]
        public void CheckLoad()
        {
            DataToTest.GetDataToTest();

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
    }
}