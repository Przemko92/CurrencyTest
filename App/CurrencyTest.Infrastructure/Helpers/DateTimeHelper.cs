using System;
using System.Collections.Generic;

namespace CurrencyTest.Infrastructure.Helpers
{
    public static class DateTimeHelper
    {
        public static IEnumerable<DateTime> GetDatesCollection(DateTime startDate, DateTime endDate)
        {
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
    }
}
