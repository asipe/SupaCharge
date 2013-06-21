using System;
using System.Diagnostics;

namespace SupaCharge.Core.DiagnosticAbstractions {
  public class DotNetStopwatch : IStopwatch {
    public DotNetStopwatch() : this(new Stopwatch()) {}

    public DotNetStopwatch(Stopwatch watch) {
      mWatch = watch;
    }

    public TimeSpan Elapsed {
      get {return mWatch.Elapsed;}
    }

    public long ElapsedMilliseconds {
      get {return mWatch.ElapsedMilliseconds;}
    }

    public long ElapsedTicks {
      get {return mWatch.ElapsedTicks;}
    }

    public bool IsRunning {
      get {return mWatch.IsRunning;}
    }

    public void Reset() {
      mWatch.Reset();
    }

    public void Start() {
      mWatch.Start();
    }

    public void Stop() {
      mWatch.Stop();
    }

    public static DotNetStopwatch StartNew() {
      return new DotNetStopwatch(Stopwatch.StartNew());
    }

    private readonly Stopwatch mWatch;
  }
}