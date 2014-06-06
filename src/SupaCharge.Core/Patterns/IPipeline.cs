namespace SupaCharge.Core.Patterns {
  public interface IPipeline<T> {
    void Execute(T context);
    void Execute(T context, ICancelToken token);
  }
}