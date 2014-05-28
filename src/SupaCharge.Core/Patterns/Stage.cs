namespace SupaCharge.Core.Patterns {
  public abstract class Stage<T> : CancellableStage<T> {
    protected Stage(int priority) : base(priority) {}

    public override void Execute(T context, CancelToken token) {
      DoExecute(context);
    }

    protected abstract void DoExecute(T context);
  }
}