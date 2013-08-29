using System;
using System.Threading;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class EmptyFuture {
    public void Set() {
      lock (mLock) {
        mEvent.Set();
      }
    }

    public void Failed(Exception exception) {
      lock (mLock) {
        mException = exception;
        mEvent.Set();
      }
    }

    public void Wait() {
      Wait(Timeout.Infinite);
    }

    public void Wait(int millisecondsTimeout) {
      if (!mEvent.WaitOne(millisecondsTimeout))
        throw new TimeoutException("Timeout waiting for future to resolve");
      ThrowIfFailed();
    }

    private void ThrowIfFailed() {
      lock (mLock) {
        if (mException != null)
          ThrowException();
      }
    }

    private void ThrowException() {
      throw new FutureException("Error Resolving Future", mException);
    }

    private readonly ManualResetEvent mEvent = new ManualResetEvent(false);
    private readonly object mLock = new Object();
    private Exception mException;
  }
}