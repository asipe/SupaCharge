namespace SupaCharge.Core.Patterns {
  public interface IStage<T> {
    int Priority{get;}
    void Execute(CancelToken token, T context);
  }
}