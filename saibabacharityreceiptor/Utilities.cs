using System;
using Enumerable = System.Linq.Enumerable;

namespace saibabacharityreceiptor
{
    public class Utilities
    {
        public static string GenerateReceiptId()
        {
            var currentUtcTime = DateTime.Now.ToUniversalTime();
            return "SAI" + currentUtcTime.ToString("yyyy") +
                   string.Concat(Enumerable.Repeat('0', 3 - currentUtcTime.DayOfYear.ToString().Length)) +
                   currentUtcTime.DayOfYear +
                   string.Concat(Enumerable.Repeat('0', 2 - currentUtcTime.Hour.ToString().Length)) +
                   currentUtcTime.Hour +
                   string.Concat(Enumerable.Repeat('0', 2 - currentUtcTime.Minute.ToString().Length)) +
                   currentUtcTime.Minute +
                   string.Concat(Enumerable.Repeat('0', 2 - currentUtcTime.Second.ToString().Length)) +
                   currentUtcTime.Second;
        }
    }
}