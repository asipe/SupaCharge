using System;

namespace SupaCharge.Core.DiagnosticAbstractions {
  public interface IStopwatch {
    TimeSpan Elapsed{get;}
    long ElapsedMilliseconds{get;}
    long ElapsedTicks{get;}
    bool IsRunning{get;}
    void Reset();
    void Start();
    void Stop();
  }
}