using System;
using System.Collections.Generic;
using System.Text;

namespace BaseX.Utils
{
    public static class DateUtils
    {
        public static int DaysBetween(DateTime sourceDate, DateTime targetDate)
        {
            return (targetDate.Date - sourceDate.Date).Days;
        }

        public static int DaysTo(DateTime targetDate)
        {
            return DaysBetween(DateTime.Today, targetDate);
        }

        public static int DaysFrom(DateTime sourceDate)
        {
            return DaysBetween(sourceDate, DateTime.Today);
        }

        public static int DaysInYear(int year)
        {
            var thisYear = new DateTime(year, 1, 1);
            var nextYear = new DateTime(year + 1, 1, 1);

            return (nextYear - thisYear).Days;
        }

        public static int DaysInYear()
        {
            return DaysInYear(DateTime.Today.Year);
        }

        public static List<DateTime> EveryDayName
        (
            DateTime start,
            DateTime end,
            HashSet<DayOfWeek> days
        )
        {
            var dateList = new List<DateTime>();

            // Make sure start is before end
            if (start > end)
            {
                var tmp = end.Date;
                end = start.Date;
                start = tmp;
            }

            // Loop through all dates, and add any that match the criteria
            for (var date = start; date <= end; date = date.AddDays(1))
                if (days.Contains(date.DayOfWeek))
                    dateList.Add(date.Date);

            return dateList;
        }

        public static long EpochTimeMillisecond()
        {
            var nowTime = DateTime.Now;
            //nowTime = nowTime.Add(TimeSpan.FromMinutes(60));
            return (long) (nowTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local)).TotalMilliseconds;
        }

        public static int EpochTime()
        {
            return (int) (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local)).TotalSeconds;
        }

        public static DateTime FromEpochTime(long epochTime)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local) + TimeSpan.FromSeconds(epochTime));
        }

        public static string FormatDays(double time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes);
        }

        public static string FormatHours(double time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public static string FormatMinutes(double time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

        public static string SecondToReadablePartString(float second)
        {
            var timeSpan = TimeSpan.FromSeconds(second);
            var dayText = timeSpan.Days > 1 ? "days" : "day";
            var hourText = timeSpan.Hours > 1 ? "hours" : "hour";
            var minuteText = timeSpan.Minutes > 1 ? "minutes" : "minute";
            var secondText = timeSpan.Seconds > 1 ? "seconds" : "second";

            if (timeSpan.Days > 0)
            {
                if (timeSpan.Days > 1)
                {
                    return timeSpan.ToString($"''dd' {dayText}'");
                }
                else
                {
                    return timeSpan.ToString($"''dd' {dayText} 'hh' {hourText}'");
                }
            }

            if (timeSpan.Hours > 0)
            {
                if (timeSpan.Minutes > 1)
                {
                    return timeSpan.ToString($"''h' {hourText} 'mm' {minuteText}'");
                }
                else
                {
                    return timeSpan.ToString($"''h' {hourText}'");
                }
            }

            if (timeSpan.Minutes > 0)
            {
                return timeSpan.ToString($"''mm' {minuteText} 'ss' {secondText}'");
            }

            return timeSpan.ToString($"''ss' {secondText}'");
        }

        public static string SecondToReadableStringWithSymbol(float second)
        {
            var timeSpan = TimeSpan.FromSeconds(second);
            var dayText = "d";
            var hourText = "h";
            var minuteText = "m";
            var secondText = "s";

            if (timeSpan.Days > 0)
            {
                if (timeSpan.Days > 1)
                {
                    return timeSpan.ToString($"''d'{dayText}'");
                }
                else
                {
                    return timeSpan.ToString($"''d'{dayText} 'h'{hourText}'");
                }
            }

            if (timeSpan.Hours > 0)
            {
                if (timeSpan.Minutes > 1)
                {
                    return timeSpan.ToString($"''h'{hourText} 'm'{minuteText}'");
                }
                else
                {
                    return timeSpan.ToString($"''h'{hourText}'");
                }
            }

            if (timeSpan.Minutes > 0)
            {
                if (timeSpan.Seconds > 0)
                    return timeSpan.ToString($"''m'{minuteText} 'ss'{secondText}'");

                return timeSpan.ToString($"''m'{minuteText}'");
            }

            return timeSpan.ToString($"''ss'{secondText}'");
        }

        public static string ConvertSecondsToTime(float totalSeconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(totalSeconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        public static string SecondToMinuteReadableString(float second)
        {
            var timeSpan = TimeSpan.FromSeconds(second);
            var minuteText = "m";

            return timeSpan.ToString($"''m'{minuteText}'");
        }

        // Usage: DateUtils.GetSecondToReadableString(time);
        public static string SecondToReadableString(float second)
        {
            StringBuilder sb = new StringBuilder();
            var timeSpan = TimeSpan.FromSeconds(second);
            var dayText = timeSpan.Days > 1 ? "days" : "day";
            var hourText = timeSpan.Hours > 1 ? "hours" : "hour";
            var minuteText = timeSpan.Minutes > 1 ? "min" : "min";
            var secondText = timeSpan.Seconds > 1 ? "sec" : "sec";

            if (timeSpan.Days > 0)
            {
                sb.Append(timeSpan.ToString($"''%d' {dayText}'"));
            }

            if (timeSpan.Hours > 0)
            {
                sb.Append(timeSpan.ToString($"''%h' {hourText}'"));
            }

            if (timeSpan.Minutes > 0)
            {
                sb.Append(timeSpan.ToString($"''%m' {minuteText}'"));
            }

            if (timeSpan.Seconds > 0)
            {
                sb.Append(timeSpan.ToString($"''%s' {secondText}'"));
            }

            return sb.ToString();
        }
    }
}