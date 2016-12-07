using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViajarSoft.Extensions
{
    public static class IEnumerableExtension
    {
        public static string JoinString(this IEnumerable<string> THIS, string separator)
        {
            return string.Join(separator, THIS);
        }
    }
}