using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Net.Bluewalk.LidlPlus
{
    public static class StringExtensions
    {
        public static string PadBoth(this string str, int length)
        {
            var spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }

        public static string Br2Nl(this string str)
        {
            return Regex.Replace(str, "<br\\s*/?>", Environment.NewLine);
        }

        public static string StripTags(this string str)
        {
            return Regex.Replace(str, "<[^>]*(>|$)", string.Empty);
        }
    }
}
