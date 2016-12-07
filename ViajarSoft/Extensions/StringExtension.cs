using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViajarSoft.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string THIS)
        {
            return string.IsNullOrEmpty(THIS);
        }
        public static string SubstringWithoutError(this string THIS, int startIndex, int length)
        {
            startIndex = Math.Min(startIndex, THIS.Length);
            length = Math.Min(length, THIS.Length - startIndex);

            return THIS.Substring(startIndex, length);
        }
    }
}