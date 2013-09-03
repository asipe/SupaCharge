namespace SupaCharge.Core.Patterns {
  public class CancelToken {
    public bool Cancelled{get;set;}

    public void Cancel() {
      Cancelled = true;
    }
  }
}