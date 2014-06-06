namespace SupaCharge.Core.Patterns {
  public interface ICancelToken {
    bool Cancelled{get;set;}
    void Cancel();
  }
}