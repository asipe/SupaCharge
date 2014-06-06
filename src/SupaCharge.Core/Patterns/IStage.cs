namespace SupaCharge.Core.Patterns {
  public interface IStage<T> {
    int Priority{get;}
    void Execute(T context, CancelToken token);
    void Execute(T context);
  }
}