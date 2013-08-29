using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class WorkQueueBatch {
    public WorkQueueBatch(IWorkQueue queue) {
      mQueue = queue;
    }

    public WorkQueueBatch Add(params Action[] work) {
      mFutures.AddRange(work.Select(w => mQueue.Enqueue(w)).ToArray());
      return this;
    }

    public WorkQueueBatch Add<T>(T data, Action<T> work) {
      mFutures.Add(mQueue.Enqueue(data, work));
      return this;
    }

    public WorkQueueBatch Wait(int millisecondsTimeout) {
      Array.ForEach(mFutures.ToArray(), future => future.Wait(millisecondsTimeout));
      return this;
    }

    private readonly List<EmptyFuture> mFutures = new List<EmptyFuture>();
    private readonly IWorkQueue mQueue;
  }
}