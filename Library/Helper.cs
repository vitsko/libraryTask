namespace Library
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class Helper
    {
        internal static char[] Comma = { ',' };

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

            string deleteGapBetween = Regex.Replace(expression, patternBetween, string.Empty);

            string result = Regex.Replace(deleteGapBetween, patternBeginninrOrEndString, string.Empty);

            return result;
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
