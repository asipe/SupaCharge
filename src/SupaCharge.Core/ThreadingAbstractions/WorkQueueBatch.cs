using System;
using System.Collections.Generic;
using System.Linq;
using SupaCharge.Core.ExceptionHandling;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class WorkQueueBatch {
    public WorkQueueBatch(IWorkQueue queue) {
      mQueue = queue;
    }

    public int PendingFutureCount {
      get {
        lock (mLock) {
          return mFutures.Count();
        }
      }
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

    public WorkQueueBatch WaitAll(int millisecondsTimeout) {
      var activityMonitor = new ActivityMonitor();
      Array.ForEach(CopyAndClearFutures(), future => activityMonitor.Monitor(() => future.Wait(millisecondsTimeout)));
      activityMonitor.Resolve();
      return this;
    }

    private EmptyFuture[] CopyAndClearFutures() {
      EmptyFuture[] futures;
      lock (mLock) {
        futures = mFutures.ToArray();
        mFutures.Clear();
      }
      return futures;
    }

    private readonly List<EmptyFuture> mFutures = new List<EmptyFuture>();
    private readonly object mLock = new object();
    private readonly IWorkQueue mQueue;
  }
}