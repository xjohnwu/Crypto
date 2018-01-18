using System;

namespace Crypto.Lib
{
    public class DateHelper
    {
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromEpochTime(long epochTime)
        {
            return _epoch.AddSeconds(epochTime);
        }

        public static long ToEpochTime(DateTime dateTime)
        {
            var utc = HandleDate(dateTime).Date;
            return (long) (utc - _epoch).TotalSeconds;
        }

        public static DateTime HandleDate(DateTime from)
        {
            switch (@from.Kind)
            {
                case DateTimeKind.Local:
                    return @from.ToUniversalTime();
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(@from, DateTimeKind.Utc);
                default:
                    return @from;
            }
        }
    }
}
