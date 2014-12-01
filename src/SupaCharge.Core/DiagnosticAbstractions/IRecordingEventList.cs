namespace SupaCharge.Core.DiagnosticAbstractions {
  public interface IRecordingEventList {
    void AddEvent(string msg, params object[] args);
    string[] GetEvents();
  }
}