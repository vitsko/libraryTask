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
        private const byte LengthISBN10 = 10,
                           LengthISBN13 = 13,
                           LengthISSN = 8,
                           EvenPosition = 3,
                           Mod10 = 10,
                           Mod11 = 11;

        private const string OneSpace = @" ",
                             UtilityPatent = @"^[1-9]([0-9]{5}|[0-9]{6})",
                             PatentWith6d = @"^(RE|PP|AI)\d{6}",
                             PatentWith7d = @"^[DXHT]\d{7}",
                             PatentByYear = @"^[0-9]{1,}-(((195|196|197|198|199)[0-9]{1})||2[0-9]{3})/[0-9]{1,}";

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

            string deleteGapBetween = Regex.Replace(expression, patternBetween, OneSpace);

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

        public static bool IsMoreThanZero(string parseToInt, out double intValue)
        {
            return double.TryParse(parseToInt, out intValue) && intValue > 0;
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

            var typebyInt = 0d;

            if (toParse.Count() == 2)
            {

                if (Helper.IsMoreThanZero(toParse[1], out typebyInt))
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

        public static bool IsISBN(string ISBN)
        {
            return Helper.IsISBN10(ISBN) || IsISBN13(ISBN);
        }

        public static bool IsISSN(string ISSN)
        {
            if (Helper.EqualsLength(ISSN, Helper.LengthISSN))
            {
                var value = 0d;

                if (Helper.IsMoreThanZero(ISSN, out value))
                {
                    var sum = Helper.SumOfDigitByPosition(ISSN, Helper.LengthISSN) -
                              int.Parse(ISSN[ISSN.Length - 1].ToString());

                    var mod11 = (byte)(sum % Helper.Mod11);
                    byte check = 0;

                    return Helper.ISCheckDigit(mod11, check, Helper.Mod11, ISSN);
                }
            }

            return false;
        }

        public static bool IsRegNum(string regNumber)
        {
            return Regex.IsMatch(regNumber, Helper.UtilityPatent) ||
                   Regex.IsMatch(regNumber, Helper.PatentWith6d) ||
                   Regex.IsMatch(regNumber, Helper.PatentWith7d) ||
                   Regex.IsMatch(regNumber, Helper.PatentByYear);
        }

        private static bool EqualsLength(string toCompare, byte length)
        {
            return toCompare.Length == length;
        }

        private static bool IsISBN10(string ISBN)
        {
            if (Helper.EqualsLength(ISBN, Helper.LengthISBN10))
            {
                var value = 0d;

                if (Helper.IsMoreThanZero(ISBN, out value))
                {
                    var sum = Helper.SumOfDigitByPosition(ISBN, Helper.LengthISBN10);

                    return sum % Helper.Mod11 == 0;
                }
            }

            return false;
        }

        private static int SumOfDigitByPosition(string toParse, byte length)
        {
            int sum = 0;

            for (byte i = length, j = 0; i > 0; i--, j++)
            {
                sum += i * int.Parse(toParse[j].ToString());
            }

            return sum;
        }

        private static bool IsISBN13(string ISBN)
        {
            if (Helper.EqualsLength(ISBN, Helper.LengthISBN13))
            {
                var value = 0d;

                if (Helper.IsMoreThanZero(ISBN, out value))
                {
                    int sum = 0;

                    for (int i = 1; i < Helper.LengthISBN13; i++)
                    {
                        var digit = int.Parse(ISBN[i - 1].ToString());

                        if (i % 2 == 0)
                        {
                            sum += digit * Helper.EvenPosition;
                        }
                        else
                        {
                            sum += digit;
                        }
                    }

                    byte check = 0;
                    var mod10 = (byte)(sum % Helper.Mod10);

                    return Helper.ISCheckDigit(mod10, check, Helper.Mod10, ISBN);
                }
            }

            return false;
        }

        private static bool ISCheckDigit(byte mod, byte check, byte minuend, string toParse)
        {
            if (mod != 0)
            {
                check = (byte)(minuend - mod);
            }

            return byte.Parse(toParse[toParse.Length - 1].ToString()) == check;
        }

        private static List<string> ParseToSign(string toParse, string[] spliter)
        {
            return new List<string>(toParse.Split(spliter, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}