using System;
using System.Collections.Generic;
using System.Linq;

namespace Nextmethod.Cex
{
    internal static class KvPair
    {

        public static KeyValuePair<TKey, TValue> New<TKey, TValue>(TKey key, TValue val)
        {
            return new KeyValuePair<TKey, TValue>(key, val);
        }

    }
}
