namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
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

        [XmlElement(ElementName = "Catalog")]
        public static List<ItemCatalog> AllItem
        {
            get
            {
                return Catalog.libraryItem;
            }

            private set
            {
                Catalog.libraryItem = value;
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

        public static List<ItemCatalog> OnlyBooks
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

        public static void RewriteCatalog(List<ItemCatalog> fromXML)
        {
            Catalog.DeleteAll();
            Catalog.AllItem = new List<ItemCatalog>(fromXML);
            Catalog.AddDefaultValue();
        }

        private static void AddDefaultValue()
        {
            foreach (var item in Catalog.AllItem)
            {
                item.CheckFromXML();
            }
        }

        private static string GetKeyForgrouping(dynamic group, GroupingBy propertyToGrouping)
        {
            return propertyToGrouping == GroupingBy.Publisher ?
                   ((IGrouping<string, Book>)group).Key :
                   ((IGrouping<int, ItemCatalog>)group).Key.ToString();
        }
    }
}