using System;
using System.Linq;

namespace Nextmethod.Cex
{
    internal static class JsonHelpers
    {

        internal static bool ContainsProperty(dynamic This, string name)
        {
            if (This == null || string.IsNullOrEmpty(name)) return false;
            var jo = This as JsonObject;
            if (jo != null)
            {
                return jo.ContainsKey(name);
            }

            var ja = This as JsonArray;
            if (ja != null)
            {
                return false;
            }

            if ((This is bool) || Equals(This, bool.FalseString) || Equals(This, bool.TrueString)) return false;

            return This.GetType().GetProperty(name);
        }

    }
}
