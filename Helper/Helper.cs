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

        public enum TypeItem
        {
            Book = 1,
            Newspaper = 2,
            Patent = 3
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
            return int.TryParse(parseToInt, out intValue) && intValue > 0;// ? true : false;
        }

        public static bool IsDate(string date, out DateTime enterDate)
        {
            CultureInfo newCulture = new CultureInfo("ru-RU", true);

            return DateTime.TryParseExact(date, "d", newCulture, DateTimeStyles.AllowWhiteSpaces, out enterDate);
        }

        public static bool IsTypeOfItemCatalog(string fromFile, out List<string> afterParsing)
        {
            afterParsing = Helper.ParseToSign(fromFile, charp);
            var type = afterParsing.ElementAt(0);
            var toParse = Helper.ParseToSign(type, colon);

            int typebyInt = 0;

            if (toParse.Count() == 2)
            {

                if (Helper.IsIntMoreThanZero(toParse[1], out typebyInt))
                {
                    return typebyInt.Equals((int)TypeItem.Book)
                        || typebyInt.Equals((int)TypeItem.Newspaper)
                        || typebyInt.Equals((int)TypeItem.Patent);
                }
            }

            return false;
        }

        public static List<string> ParseStringToItem(List<string> oneStringValue)
        {
            var valuesOfItemCatalog = new List<string>();

            for (var index = 0; index < oneStringValue.Count; index++)
            {
                var stringValue = Helper.ParseToSign(oneStringValue[index], Helper.colon);

                if (stringValue.Count != 2)
                {
                    valuesOfItemCatalog.Add(string.Empty);
                }
                else
                {
                    valuesOfItemCatalog.Add(stringValue[1]);
                }
            }

            return valuesOfItemCatalog;
        }

        public static void AddStringsForCheck(List<string> aboutItemCatalog, int countOfValues)
        {
            var diff = countOfValues - aboutItemCatalog.Count;

            if (diff > 0)
            {
                for (var i = 0; i < diff; i++)
                {
                    aboutItemCatalog.Add(string.Empty);
                }
            }
            else
            {
                if (diff < 0)
                {
                    aboutItemCatalog.RemoveRange(countOfValues, -1 * diff);
                }
            }
        }

        private static List<string> ParseToSign(string toParse, string[] spliter)
        {
            return new List<string>(toParse.Split(spliter, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}