using System;

namespace SupaCharge.Core.DateAndTime.Extensions {
  public static class GetMonthEndExtension {
    public static DateTime GetMonthEndDate(this DateTime dt) {
      return dt.AddDays(DateTime.DaysInMonth(dt.Year, dt.Month) - dt.Day);
    }
  }
}