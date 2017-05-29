namespace Helper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class Helper
    {
        private const char Comma = ',';
        private static string oneSpace = @" ";
        private static string[] charp = { "#" };
        private static string[] colon = { ":" };
        private static string[] typeItem = { "книга", "газета", "патент" };

        public enum TypeItem
        {
            Book = 1,
            Newspaper,
            Patent,
            Default
        }

        public static List<string> GetyQuestions(string questions)
        {
            return new List<string>(questions.Split(Helper.Comma));
        }

        public static bool Contains(this string str, string substring, StringComparison comp)
        {
            return str.IndexOf(substring, comp) >= 0;
        }

        public static string DeleteWhitespace(string expression)
        {
            ///Паттерн поиска пробелов между словами
            string patternBetween = @"\s+";

            ///Паттерн поиска пробелов в начале\конце строки
            string patternBeginninrOrEndString = @"(^(\s+))?((\s+)$)?";

            string deleteGapBetween = Regex.Replace(expression, patternBetween, oneSpace);

            return Regex.Replace(deleteGapBetween, patternBeginninrOrEndString, string.Empty);
        }

        public static List<string> DeleteEmpty(List<string> item)
        {
            item.RemoveAll(it => string.IsNullOrWhiteSpace(it));

            var newItem = new List<string>();

            item.ForEach(delegate (string oneItem)
                {
                    oneItem = Helper.DeleteWhitespace(oneItem);
                    newItem.Add(oneItem);
                });

            return newItem;
        }

        public static bool IsIntMoreThanZero(string parseToInt, out int intValue)
        {
            return int.TryParse(parseToInt, out intValue) && intValue > 0 ? true : false;
        }

        public static bool IsDateAsDDMMYYYY(string date, out DateTime enterDate)
        {
            CultureInfo newCulture = new CultureInfo("ru-RU", true);

            return DateTime.TryParseExact(date, "d", newCulture, DateTimeStyles.AllowWhiteSpaces, out enterDate);
        }

        public static bool IsTypeOfItemCatalog(string fromFile, out List<string> afterParsing)
        {
            var toCompare = fromFile.ToLower();

            afterParsing = Helper.ParseToSign(toCompare, charp);
            var type = afterParsing.ElementAt(0);

            return type.Equals(typeItem[0])
                || type.Equals(typeItem[1])
                || type.Equals(typeItem[2]);
        }

        public static List<string> ParseStringToItem(List<string> oneStringValue)
        {
            var valuesOfItemCatalog = new List<string>();

            for (var index = 0; index < oneStringValue.Count; index++)
            {
                if (index == 0)
                {
                    valuesOfItemCatalog.Add(oneStringValue[index]);
                }

                if (index != 0)
                {
                    var stringValue = Helper.ParseToSign(oneStringValue[index], Helper.colon);

                    if (stringValue.Count == 0 || stringValue.Count == 1)
                    {
                        valuesOfItemCatalog.Add(string.Empty);
                    }
                    else
                    {
                        valuesOfItemCatalog.Add(stringValue[1]);
                    }

                }

            }

            return valuesOfItemCatalog;
        }

        public static byte GetTypeItem(string typeItemCatalog)
        {
            var type = typeItemCatalog.ToLower();

            if (type.Equals(typeItem[0]))
            {
                return (byte)TypeItem.Book;
            }

            if (type.Equals(typeItem[1]))
            {
                return (byte)TypeItem.Newspaper;
            }

            if (type.Equals(typeItem[2]))
            {
                return (byte)TypeItem.Patent;
            }

            return (byte)TypeItem.Default;

        }

        public static void AddValuesForCheckImport(List<string> aboutItemCatalog, int countOfValues)
        {
            var count = aboutItemCatalog.Count - 1;

            while (countOfValues != count)
            {
                aboutItemCatalog.Add(string.Empty);
                count++;
            }
        }


        private static List<string> ParseToSign(string toParse, string[] spliter)
        {
            return new List<string>(toParse.Split(spliter, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}