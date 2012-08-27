using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupaCharge.Core.Collections.Extensions {
  public static class TruncateExtension {
    public static IEnumerable<T> Truncate<T>(this IEnumerable<T> item, int amount) {
      var count = item.Count();
      return item.TakeWhile((i, x) => x < count - amount);
    }
  }

}
