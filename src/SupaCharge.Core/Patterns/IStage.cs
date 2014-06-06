namespace SupaCharge.Core.Patterns {
  public interface IStage<T> {
    int Priority{get;}
    void Execute(T context, ICancelToken token);
    void Execute(T context);
  }
}