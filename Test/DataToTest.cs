namespace Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Library;

    internal static class DataToTest
    {
        internal const string ToCompareForSave = "#Тип записи:1#Название:Название книги#Авторы:Другой автор-1,Михаил Булгаков,Толстой#Место издательства (Город):Ижевск#Издательство:Московский вестник#Год издания:2000#Количество страниц:250#Примечание:Примечание для книги#Международный стандартный номер книги (ISBN):978-3-16-148410-0#\r\n";

        internal static List<List<string>> ToLoad
        {
            get
            {
                var data = new List<string>
            {
                "1",
                "Название книги",
                "Другой автор-1,Михаил Булгаков,Толстой",
                "Ижевск",
                "Московский вестник",
                "2000",
                "250",
                "Примечание для книги",
                "978-3-16-148410-0"
             };

                var toLoad = new List<List<string>>
                {
                    data
                };

                return toLoad;
            }
        }

        internal static List<List<string>> IncorrectToLoad
        {
            get
            {
                var data = new List<string>
            {
                "1",
                string.Empty,
                "Другой автор-1,Михаил Булгаков,Толстой",
                "Ижевск",
                "Московский вестник",
                "2000",
                "250",
                "Примечание для книги",
                "9780141180100"
             };

                var toLoad = new List<List<string>>
                {
                    data
                };

                return toLoad;
            }
        }

        internal static List<List<string>> InfoToCatalog()
        {
            List<List<string>> data = new List<List<string>>
            {
                null,
                null,
                null,
                null,
                null,
                null,
                null
            };

            data[0] = new List<string>
            {
            string.Empty,
            "Михаил Булгаков",
            "Екатеринбург",
            "Москва",
            "1980",
            "310",
            "Роман в 32 главы",
            "9780141180144"
            };

            data[1] = new List<string>
            {
            "Советский Спорт",
            "Москва",
            "Советский Спорт",
            "2013",
            "50",
            "Газета за 16 декабря, №189-М(19149)",
            "19149",
            "16.12.2013",
            "84025613"
            };

            data[2] = new List<string>
            {
            "Способ запуска объекта с космодрома в космосе",
            "Ломанов А.А.",
            "Россия",
            "2241643-2002/1",
            "04.12.2000",
            "27.11.2002",
            "500",
            "Изобретение относится к области космической техники и, в частности, к средствам запуска космических объектов"
             };

            data[3] = new List<string>
            {
            "Название книги",
            "Другой автор-1,Михаил Булгаков,Толстой",
            "Ижевск",
            "Московский вестник",
            "2000",
            "250",
            "Примечание для книги",
            "978-3-16-148410-0"
             };

            data[4] = new List<string>
            {
            "Спорт",
            "Санкт-Петербург",
            "Совет",
            "2000",
            "50",
            "Хорошая газета",
            "19111",
            "16.10.2007",
            "84025677"
            };

            data[5] = new List<string>
            {
            "Патент",
            "Ломанов А.А., Еще один изобретатель",
            "Россия",
            "2241677-2012/4",
            "11.10.2001",
            "27.01.2002",
            "300",
            "Примечание к патерну за номером 2241677-2012/4"
            };

            // Данные для редактирования
            data[6] = new List<string>
            {
            "Мастер",
            "Михаил Булгаков",
            "Екатеринбург",
            "Москва",
            "1980",
            "310",
            "Роман в 32 главы",
            "978-3-16-148410-0"
           };

            return data;
        }

        internal static void GetDataToTest()
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

        internal static List<ItemCatalog> GetDataToSearchOrSort()
        {
            var catalog = new List<ItemCatalog>
            {
                new Book(DataToTest.InfoToCatalog()[0]),
                new Book(DataToTest.InfoToCatalog()[3]),
                new Newspaper(DataToTest.InfoToCatalog()[1]),
                new Newspaper(DataToTest.InfoToCatalog()[4]),
                new Patent(DataToTest.InfoToCatalog()[2]),
                new Patent(DataToTest.InfoToCatalog()[5])
            };

            return catalog;
        }

        internal static List<ItemCatalog> OnlyBook()
        {
            return DataToTest.GetDataToSearchOrSort().Where(item => item is Book).ToList();
        }
    }
}