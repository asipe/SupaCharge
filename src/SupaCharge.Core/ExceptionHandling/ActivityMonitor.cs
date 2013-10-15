using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.ExceptionHandling {
  public class ActivityMonitor {
    public void Resolve() {
      if (mErrors.Any())
        throw new AggregatedException(mErrors.ToArray(), "{0} Activities Failed", mErrors.Count());
    }

    public void Monitor(Action activity) {
      try {
        activity();
      } catch (Exception e) {
        mErrors.Add(e);
      }
    }

    private readonly List<Exception> mErrors = new List<Exception>();
  }
}