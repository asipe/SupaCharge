using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.ExceptionHandling {
  public class ActivityMonitor {
    public void Resolve() {
      if (mErrors.Any())
        throw new AggregatedException(mErrors.ToArray(), "{0} of {1} Activities Failed", mErrors.Count(), mActivityCount);
    }

    public void Monitor(Action activity) {
      ++mActivityCount;
      try {
        activity();
      } catch (Exception e) {
        mErrors.Add(e);
      }
    }

    private int mActivityCount;
    private readonly List<Exception> mErrors = new List<Exception>();
  }
}