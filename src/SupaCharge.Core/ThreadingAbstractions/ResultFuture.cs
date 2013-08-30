using System;
using System.Threading;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class ResultFuture<T> : IDisposable {
    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

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

    protected virtual void Dispose(bool disposing) {
      if (mEvent == null || !disposing)
        return;

      mEvent.Close();
      mEvent = null;
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

    private readonly object mLock = new Object();
    private ManualResetEvent mEvent = new ManualResetEvent(false);
    private Exception mException;
    private T mValue;
  }
}