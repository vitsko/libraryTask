namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public static class Helper
    {
        internal static char[] Comma = { ',' };
        private static string oneSpace = @" ";
        private static string withoutSpace = @"";

        public static bool Contains(this string str, string substring, StringComparison comp)
        {
            return str.IndexOf(substring, comp) >= 0;
        }

        internal static string DeleteWhitespace(string expression)
        {
            ///Паттерн поиска пробелов между словами
            string patternBetween = @"\s+";

            ///Паттерн поиска пробелов в начале\конце строки
            string patternBeginninrOrEndString = @"(^(\s+))?((\s+)$)?";

            string deleteGapBetween = Regex.Replace(expression, patternBetween, oneSpace);

            return Regex.Replace(deleteGapBetween, patternBeginninrOrEndString, withoutSpace);
        }

        internal static bool IsIntMoreThanZero(string parseToInt, out int intValue)
        {
            return int.TryParse(parseToInt, out intValue) && intValue > 0 ? true : false;
        }

        internal static bool IsDateAsDDMMYYYY(string date, out DateTime enterDate)
        {
            CultureInfo newCulture = new CultureInfo("en-US", true);
            newCulture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";

            return DateTime.TryParseExact(date, newCulture.DateTimeFormat.ShortDatePattern, newCulture, DateTimeStyles.AllowWhiteSpaces, out enterDate) ? true : false;
        }
    }

    internal class ComparatorByContains : IEqualityComparer<string>
    {
        public bool Equals(string withinStr, string search)
        {
            return Helper.Contains(withinStr, search, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string comparator)
        {
            return comparator.GetHashCode();
        }
    }
}