using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.Collections.Extensions {
  public static class ElementsAtExtension {
    public static IEnumerable<T> ElementsAt<T>(this IEnumerable<T> items, params int[] indexes) {
      return indexes.Select(x => items.ElementAt(x));
    }
  }
}