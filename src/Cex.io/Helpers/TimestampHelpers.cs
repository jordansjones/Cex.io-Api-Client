using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public static class TimestampHelpers
    {

        internal static readonly DateTime EpochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime TimestampToUtcDateTime(uint timestamp)
        {
            return EpochStart.AddSeconds(timestamp).ToUniversalTime();
        }

        public static uint UtcDateTimeToTimestamp(DateTime? value = null)
        {
            var dateTime = value != null ? value.Value : DateTime.UtcNow;
            return Convert.ToUInt32((dateTime - EpochStart).TotalSeconds);
        }

        public static uint ToTimestamp(this DateTime This)
        {
            return UtcDateTimeToTimestamp(This.ToUniversalTime());
        }

    }
}
