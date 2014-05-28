namespace SupaCharge.Core.Patterns {
  public abstract class CancellableStage<T> : IStage<T> {
    protected CancellableStage(int priority) {
      Priority = priority;
    }

    public int Priority{get;private set;}
    public abstract void Execute(T context, CancelToken token);
  }
}