namespace SupaCharge.Core.DiagnosticAbstractions {
  public interface IRecordingEventList {
    void AddEvent(string msg);
    string[] GetEvents();
  }
}