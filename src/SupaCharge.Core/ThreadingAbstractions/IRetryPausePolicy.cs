namespace SupaCharge.Core.ThreadingAbstractions {
  public interface IRetryPausePolicy {
    void Pause();
  }
}