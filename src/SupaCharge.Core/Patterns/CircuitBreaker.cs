using System;
using SupaCharge.Core.DiagnosticAbstractions;

namespace SupaCharge.Core.Patterns {
  public class CircuitBreaker<T> : ICircuitBreaker<T> {
    public CircuitBreaker(IStopwatch stopwatch, Func<Exception, bool> breakCheck, int timeout, T defaultValue) {
      mStopwatch = stopwatch;
      mBreakCheck = breakCheck;
      mTimeout = timeout;
      mDefaultValue = defaultValue;
    }

    public T Execute(Func<T> work) {
      try {
        return IsCircuitClosed() ? DoWork(work) : mDefaultValue;
      } catch (Exception e) {
        if (!mBreakCheck(e))
          throw;
        OpenCircuit();
      }
      return mDefaultValue;
    }

    private bool IsCircuitClosed() {
      lock (mLock) {
        return (!mStopwatch.IsRunning || (mStopwatch.ElapsedMilliseconds > mTimeout));
      }
    }

    private T DoWork(Func<T> work) {
      var result = work();
      lock (mLock) {
        mStopwatch.Stop();
      }
      return result;
    }

    private void OpenCircuit() {
      lock (mLock) {
        mStopwatch.Reset();
        mStopwatch.Start();
      }
    }

    private readonly Func<Exception, bool> mBreakCheck;
    private readonly T mDefaultValue;
    private readonly object mLock = new object();
    private readonly IStopwatch mStopwatch;
    private readonly int mTimeout;
  }
}