using System;

namespace EDDY.IS.AdMatching.Core.Auxiliary
{
    public class CommonTimeZoneManager
    {
        public CommonTimeZoneManager()
        {
        }

        public DateTime ConvertTimeZoneToUtc(String TimeZoneString, DateTime DateTimeToConvert)
        {
            try
            {
                TimeZoneInfo TimeZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneString);
               return TimeZoneInfo.ConvertTimeToUtc(DateTimeToConvert, TimeZoneId);
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("Unable to find the {0} zone in the registry.",
                                  TimeZoneString);
                return new DateTime();

            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the {0} zone has been corrupted.",
                                  TimeZoneString);
                return new DateTime();
            }
           

          
        }

        public DateTime LocalDateTimeToUTC(DateTime NowParameter)
        {
            DateTime Now = DateTime.UtcNow;
            return Now;
        }
        public DateTime UtcToLocalDateTime(DateTime NowParameter)
        {
            DateTime Now = DateTime.UtcNow;
            return Now;
        }

        public static TimeSpan LocalTimeSpanToUTC(TimeSpan ts)
        {
            DateTime dt = new DateTime(ts.Ticks);
            DateTime dtUtc = dt.ToUniversalTime();
            TimeSpan tsUtc = dtUtc.TimeOfDay;

            return tsUtc;
        }

        public static TimeSpan UTCTimeSpanToLocal(TimeSpan tsUtc)
        {
            DateTime dtUtc = new DateTime(tsUtc.Ticks);
            DateTime dt = dtUtc.ToLocalTime();
            TimeSpan ts = dt.TimeOfDay;

            return ts;
        }


    }
}
