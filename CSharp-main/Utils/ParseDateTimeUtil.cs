using System;

namespace Utils
{
    public static class ParseDateTimeUtil
    {
        //public static DateTime GetDateOrDefault(DateTime? date)
        //{            
        //    try
        //    {
        //        if (date == null || date == default)
        //            return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        //        return DateTime.Parse(date?.ToString("yyyy-MM-dd HH:mm:ss"));
        //    }
        //    catch
        //    {
        //        return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    }

        //}
        public static DateTime GetDateOrDefault(DateTime? date)
        {
            try
            {
                if (date == null || date == default)
                    return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

                TimeZoneInfo brTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                DateTime convertedDate = TimeZoneInfo.ConvertTimeFromUtc(date.Value.ToUniversalTime(), brTimeZone);
                return DateTime.Parse(convertedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch
            {
                return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }



        public static DateTime GetDateOrDefault(string dateString)
        {
            try
            {
                if (string.IsNullOrEmpty(dateString))
                    return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                return DateTime.Parse(DateTime.Parse(dateString).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch
            {
                return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}