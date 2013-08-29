using System;
using System.Threading;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class ResultFuture<T> {
    public void Set(T value) {
      lock (mLock) {
        mValue = value;
        mEvent.Set();
      }
    }

    public void Failed(Exception exception) {
      lock (mLock) {
        mException = exception;
        mEvent.Set();
      }
    }

    public T Wait() {
      return Wait(Timeout.Infinite);
    }

    public T Wait(int millisecondsTimeout) {
      if (mEvent.WaitOne(millisecondsTimeout))
        return GetValueOrThrow();
      throw new TimeoutException("Timeout waiting for future to resolve");
    }

    private T GetValueOrThrow() {
      lock (mLock) {
        if (mException != null)
          ThrowException();
        return mValue;
      }
    }

    private void ThrowException() {
      throw new FutureException("Error Resolving Future", mException);
    }

    private readonly ManualResetEvent mEvent = new ManualResetEvent(false);
    private readonly object mLock = new Object();
    private Exception mException;
    private T mValue;
  }
}