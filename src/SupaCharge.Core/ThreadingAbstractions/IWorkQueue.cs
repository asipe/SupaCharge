using System;

namespace SupaCharge.Core.ThreadingAbstractions {
  public interface IWorkQueue {
    EmptyFuture Enqueue(Action work);
    EmptyFuture Enqueue<T>(T data, Action<T> work);
    ResultFuture<T> Enqueue<T>(Func<T> work);
    ResultFuture<TResult> Enqueue<T, TResult>(T data, Func<T, TResult> work);
  }
}