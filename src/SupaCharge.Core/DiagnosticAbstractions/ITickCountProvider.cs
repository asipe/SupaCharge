namespace SupaCharge.Core.DiagnosticAbstractions {
  public interface ITickCountProvider {
    long GetTicks();
    long GetFrequency();
  }
}