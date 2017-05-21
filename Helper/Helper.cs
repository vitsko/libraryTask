namespace Helper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class Helper
    {
        private const char Comma = ',';
        private static string oneSpace = @" ";

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
    }
}