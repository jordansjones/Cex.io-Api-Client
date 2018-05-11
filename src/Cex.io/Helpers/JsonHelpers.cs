using System;
using System.Globalization;
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

        internal static decimal ToDecimal(dynamic value)
        {
            if (value == null) return default(decimal);
            var valueType = value.GetType();

            if (valueType == typeof (decimal)) return value;
            if (valueType == typeof (double)) return (decimal)value;

            // return decimal.Parse(value, CultureInfo.InvariantCulture); This not work for me
            return Convert.ToDecimal(value); // Simple fix for me
        }

    }
}
