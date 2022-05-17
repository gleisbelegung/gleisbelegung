using System;

namespace Gleisbelegung.App.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToLogTime(this DateTime dateTime)
        {
            return dateTime.ToString("[dd.MM.yyyy HH:mm:ss.fff]");
        }
    }
}