namespace Library
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class Helper
    {
        internal static char[] Comma = { ',' };

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
}
