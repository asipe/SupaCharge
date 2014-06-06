namespace SupaCharge.Core.Patterns {
  public class CancelToken : ICancelToken {
    public bool Cancelled{get;set;}

    public void Cancel() {
      Cancelled = true;
    }
  }
}