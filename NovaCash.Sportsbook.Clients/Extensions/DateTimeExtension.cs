using System;
using NovaCash.Sportsbook.Clients.Configurations;

namespace NovaCash.Sportsbook.Clients.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime AddGMT(this DateTime? dt, DateTime defaultValue)
            => dt.HasValue ? dt.Value.AddHours(AppSettings.Settings.GMT) : defaultValue;
    }
}