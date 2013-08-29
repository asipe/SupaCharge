using System;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class SynchronousWorkQueue : IWorkQueue {
    public EmptyFuture Enqueue(Action work) {
      var future = new EmptyFuture();
      DoWork(work, future);
      return future;
    }

    public EmptyFuture Enqueue<T>(T data, Action<T> work) {
      var future = new EmptyFuture();
      DoWork(data, work, future);
      return future;
    }

    public ResultFuture<T> Enqueue<T>(Func<T> work) {
      var future = new ResultFuture<T>();
      DoWork(work, future);
      return future;
    }

    public ResultFuture<TResult> Enqueue<T, TResult>(T data, Func<T, TResult> work) {
      var future = new ResultFuture<TResult>();
      DoWork(data, work, future);
      return future;
    }

    private static void DoWork<T, TResult>(T data, Func<T, TResult> work, ResultFuture<TResult> future) {
      try {
        future.Set(work(data));
      } catch (Exception e) {
        future.Failed(e);
      }
    }

    private static void DoWork<T>(Func<T> work, ResultFuture<T> future) {
      try {
        future.Set(work());
      } catch (Exception e) {
        future.Failed(e);
      }
    }

    private static void DoWork(Action work, EmptyFuture future) {
      try {
        work();
        future.Set();
      } catch (Exception e) {
        future.Failed(e);
      }
    }

    private static void DoWork<T>(T data, Action<T> work, EmptyFuture future) {
      try {
        work(data);
        future.Set();
      } catch (Exception e) {
        future.Failed(e);
      }
    }
  }
}