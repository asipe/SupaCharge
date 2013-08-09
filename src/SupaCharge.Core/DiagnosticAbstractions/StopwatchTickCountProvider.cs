using System.Diagnostics;

namespace SupaCharge.Core.DiagnosticAbstractions {
  public class StopwatchTickCountProvider : ITickCountProvider {
    public long GetTicks() {
      return Stopwatch.GetTimestamp();
    }
  }
}