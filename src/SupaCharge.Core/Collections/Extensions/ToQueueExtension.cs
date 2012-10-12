using System.Collections.Generic;

namespace SupaCharge.Core.Collections.Extensions {
  public static class ToQueueExtension {
    public static Queue<T> ToQueue<T>(this IEnumerable<T> input) {
      return new Queue<T>(input);
    }
  }
}