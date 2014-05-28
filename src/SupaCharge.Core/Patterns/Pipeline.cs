using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.Patterns {
  public class Pipeline<T> : IPipeline<T> {
    public Pipeline(params IStage<T>[] stages) {
      mStages = SortStages(stages);
    }

    public void Execute(T context) {
      Execute(context, new CancelToken());
    }

    private static IStage<T>[] SortStages(IEnumerable<IStage<T>> stages) {
      return stages
        .OrderBy(stage => stage.Priority)
        .ToArray();
    }

    private void Execute(T context, CancelToken token) {
      foreach (var stage in mStages) {
        stage.Execute(context, token);
        if (token.Cancelled)
          break;
      }
    }

    private readonly IStage<T>[] mStages;
  }
}