namespace Helper
{
    using System;
    using System.Collections.Generic;

    public class Comparator : IEqualityComparer<string>
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