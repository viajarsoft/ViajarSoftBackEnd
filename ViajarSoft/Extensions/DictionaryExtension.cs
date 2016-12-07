using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViajarSoft.Extensions
{
    public static class DictionaryExtension
    {
        public static V ElementOrDefault<K, V>(this Dictionary<K, V> THIS, K key)
        {
            if (THIS.ContainsKey(key))
                return THIS[key];

            return default(V);
        }
    }
}