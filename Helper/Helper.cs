namespace Helper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml;

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

        public enum TypeItem : byte
        {
            Book = 1,
            Newspaper = 2,
            Patent = 3
        }

        public static XmlReader XmlRead { get; set; }

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

            string deleteGapBetween = Regex.Replace(expression, patternBetween, Helper.OneSpace);

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

        public static bool IsMoreThanZero(string parse, out dynamic value)
        {
            if (!string.IsNullOrWhiteSpace(parse) && Regex.IsMatch(parse, @"^[0-9]{1,}"))
            {
                if (parse.Length == 1)
                {
                    value = byte.Parse(parse);
                }
                else
                {
                    value = double.Parse(parse);
                }
            }
            else
            {
                value = 0;
            }

            return value != 0;
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

            dynamic typebyInt;

            if (toParse.Count() == 2)
            {
                if (Helper.IsMoreThanZero(toParse[1], out typebyInt))
                {
                    return Enum.GetValues(typeof(TypeItem)).Cast<byte>()
                                                           .ToList()
                                                           .Contains(typebyInt);
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

            if (diff == 0)
            {
                return;
            }

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

        public static bool IsISBN(string isbn)
        {
            var toParse = Helper.OnlyDigitallyValue(isbn);
            return Helper.IsISBN10(toParse) || IsISBN13(toParse);
        }

        public static bool IsISSN(string issn)
        {
            var toParse = Helper.OnlyDigitallyValue(issn);

            if (Helper.EqualsLength(toParse, Helper.LengthISSN))
            {
                dynamic value;

                if (Helper.IsMoreThanZero(toParse, out value))
                {
                    var sum = Helper.SumOfDigitByPosition(toParse, Helper.LengthISSN) -
                              int.Parse(toParse[toParse.Length - 1].ToString());

                    var mod11 = (byte)(sum % Helper.Mod11);
                    byte check = 0;

                    return Helper.ISCheckDigit(mod11, check, Helper.Mod11, toParse);
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

        private static string OnlyDigitallyValue(string value)
        {
            return Regex.Replace(value, @"-+", string.Empty);
        }

        private static bool EqualsLength(string toCompare, byte length)
        {
            return toCompare.Length == length;
        }

        private static bool IsISBN10(string isbn)
        {
            if (Helper.EqualsLength(isbn, Helper.LengthISBN10))
            {
                dynamic value;

                if (Helper.IsMoreThanZero(isbn, out value))
                {
                    var sum = Helper.SumOfDigitByPosition(isbn, Helper.LengthISBN10);

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

        private static bool IsISBN13(string isbn)
        {
            if (Helper.EqualsLength(isbn, Helper.LengthISBN13))
            {
                dynamic value;

                if (Helper.IsMoreThanZero(isbn, out value))
                {
                    int sum = 0;

                    for (int i = 1; i < Helper.LengthISBN13; i++)
                    {
                        var digit = int.Parse(isbn[i - 1].ToString());

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

                    return Helper.ISCheckDigit(mod10, check, Helper.Mod10, isbn);
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