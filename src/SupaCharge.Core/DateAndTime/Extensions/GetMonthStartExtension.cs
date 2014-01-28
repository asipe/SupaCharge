using System;

namespace SupaCharge.Core.DateAndTime.Extensions {
  public static class GetMonthStartExtension {
    public static DateTime GetMonthStartDate(this DateTime dt) {
      return new DateTime(dt.Year, dt.Month, 1);
    }
  }
}