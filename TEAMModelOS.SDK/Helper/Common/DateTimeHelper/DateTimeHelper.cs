using System;
using System.Collections.Generic;
using System.Text;

namespace TEAMModelOS.SDK.Helper.Common.DateTimeHelper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(long timeStamp)
        {
            var dtStart = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
            TimeSpan toNow = new TimeSpan(timeStamp);
            return dtStart.Add(toNow);
            //var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //return start.AddMilliseconds(timeStamp).AddHours(8);
        }
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp13(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp10(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds / 1000;
        }

        public static DateTime FromUnixTimestamp(this long unixtime)
        {
            DateTime sTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            return sTime.AddMilliseconds(unixtime);
        }

        public static DateTime FromUnixTimestampOffSet(this long unixtime, int offset)
        {
            DateTime sTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            int serverOffset = (int)TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).TotalMinutes;
            int subOffset = offset - serverOffset;
            return sTime.AddMilliseconds(unixtime).AddMinutes(subOffset);
        }
        public static long ToUnixTimestamp(this DateTime datetime)
        {
            //DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime sTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            return (long)(datetime - sTime).TotalMilliseconds;
        }
        /// <summary>
        /// 获取当前cpu振荡时间戳 17位数
        /// </summary>
        /// <returns></returns>
        public static long GetCPUMillisecond()
        {
            return DateTime.Now.ToUniversalTime().Ticks - 621355968000000000;
        }
        /// <summary>
        /// 获取标准毫秒级时间戳 13位数
        /// </summary>
        /// <returns></returns>
        public static long GetMillisecond()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }
        /// <summary>
        /// 获取当前年
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentYear()
        {
            return DateTime.Now.Year;
        }
        /// <summary>
        /// 获取当前月份
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMonth()
        {
            return DateTime.Now.Month;
        }
        /// <summary>
        /// 获取星期几
        /// </summary>
        /// <returns></returns>
        public static DayOfWeek GetCurrentDayOfWeek()
        {
            return DateTime.Now.DayOfWeek;
        }
        /// <summary>
        /// 获取本年第几天
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentDayOfYear()
        {
            return DateTime.Now.DayOfYear;
        }
        /// <summary>
        /// 获取当前小时
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentHour()
        {
            return DateTime.Now.Hour;
        }
        /// <summary>
        /// 获取当前分钟
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMinute()
        {
            return DateTime.Now.Minute;
        }
        /// <summary>
        /// 获取当前秒
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentSecond()
        {
            return DateTime.Now.Second;
        }
        /// <summary>
        /// 获得 GMT+8 时间
        /// </summary>
        /// <returns></returns>
        public static DateTime ChinaTime()
        {

            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");//设置时区
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, easternZone);
            return easternTime;
        }

    }
}
