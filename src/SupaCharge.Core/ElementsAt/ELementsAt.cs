using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupaCharge.Core.ElementsAt {
  public static class ELementsAtExtension {
    public static T[] ElementsAt<T>(this IEnumerable<T> items, params int[] indexes) {
       List<T> Result = new List<T>();
      
      for (var x = 0; x < indexes.Length; x++) {
        Result.Add(items.ElementAt(x));
      }

      return Result.ToArray() ;
    }
  }
}
