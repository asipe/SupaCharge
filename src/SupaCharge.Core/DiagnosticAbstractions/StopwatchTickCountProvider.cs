using System.Diagnostics;

namespace SupaCharge.Core.DiagnosticAbstractions {
  public class StopwatchTickCountProvider : ITickCountProvider {
    public long GetTicks() {
      return Stopwatch.GetTimestamp();
    }

    public long GetFrequency() {
      return _Frequency;
    }

    private static readonly long _Frequency = Stopwatch.Frequency;
  }
}