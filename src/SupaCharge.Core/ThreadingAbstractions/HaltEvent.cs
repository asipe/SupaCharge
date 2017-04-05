using System;
using System.Threading;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class HaltEvent : IHaltEvent, IDisposable {
    public HaltEvent() {
      mEvent = new ManualResetEvent(false);
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    public void Set() {
      mEvent.Set();
    }

    public void Reset() {
      mEvent.Reset();
    }

    public bool IsSet() {
      return mEvent.WaitOne(0);
    }

    protected virtual void Dispose(bool disposing) {
      if (!disposing)
        return;
      if (mEvent == null)
        return;
      mEvent.Close();
      mEvent = null;
    }

    private ManualResetEvent mEvent;
  }
}