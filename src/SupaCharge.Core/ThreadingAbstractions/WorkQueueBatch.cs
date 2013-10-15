using System;
using System.Collections.Generic;
using System.Linq;
using SupaCharge.Core.ExceptionHandling;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class WorkQueueBatch {
    public WorkQueueBatch(IWorkQueue queue) {
      mQueue = queue;
    }

    public WorkQueueBatch Add(params Action[] work) {
      lock (mLock) {
        mFutures.AddRange(work.Select(w => mQueue.Enqueue(w)).ToArray());
      }
      return this;
    }

    public WorkQueueBatch Add<T>(T data, Action<T> work) {
      lock (mLock) {
        mFutures.Add(mQueue.Enqueue(data, work));
      }
      return this;
    }

    public WorkQueueBatch Wait(int millisecondsTimeout) {
      lock (mLock) {
        Array.ForEach(mFutures.ToArray(), future => future.Wait(millisecondsTimeout));
      }
      return this;
    }
    
    public WorkQueueBatch WaitAll(int millisecondsTimeout) {
      lock (mLock) {
        var activityMonitor = new ActivityMonitor();
        Array.ForEach(mFutures.ToArray(), future => activityMonitor.Monitor(() => future.Wait(millisecondsTimeout)));
        activityMonitor.Resolve();
      }
      return this;
    }

    private readonly List<EmptyFuture> mFutures = new List<EmptyFuture>();
    private readonly object mLock = new object();
    private readonly IWorkQueue mQueue;
  }
}