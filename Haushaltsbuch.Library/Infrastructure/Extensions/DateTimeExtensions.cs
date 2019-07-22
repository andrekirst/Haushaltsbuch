using System;

namespace Haushaltsbuch.Library.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime) =>
            new DateTime(year: dateTime.Year, month: dateTime.Month, day: 1);

        public static DateTime LastDayOfMonth(this DateTime dateTime) =>
            FirstDayOfMonth(dateTime: dateTime)
                .AddMonths(months: 1)
                .AddDays(value: -1);
    }
}