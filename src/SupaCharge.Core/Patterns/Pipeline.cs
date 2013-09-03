namespace SupaCharge.Core.Patterns {
  public class Pipeline<T> : IPipeline<T> {
    public Pipeline(IStage<T>[] stages) {
      mStages = stages;
    }

    public void Execute(T context) {
      Execute(context, new CancelToken());
    }

    private void Execute(T context, CancelToken token) {
      foreach (var stage in mStages) {
        stage.Execute(token, context);
        if (token.Cancelled)
          break;
      }
    }

    private readonly IStage<T>[] mStages;
  }
}