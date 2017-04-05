namespace SupaCharge.Core.ThreadingAbstractions {
  public interface IHaltEvent {
    void Set();
    void Reset();
    bool IsSet();
  }
}