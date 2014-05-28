using System.Collections.Generic;

namespace SupaCharge.Core.DiagnosticAbstractions {
  public class RecordingEventList : IRecordingEventList {
    public RecordingEventList(ITickCountProvider provider) {
      mProvider = provider;
      mStart = mProvider.GetTicks();
      AddEvent(0, "Created");
    }

    public void AddEvent(string msg) {
      AddEvent(mProvider.GetTicks() - mStart, msg);
    }

    public string[] GetEvents() {
      lock (mLock) {
        return mEvents.ToArray();
      }
    }

    private void AddEvent(long ticks, string msg) {
      msg = string.Format("{0}: {1}", ticks, msg);
      lock (mLock) {
        mEvents.Add(msg);
      }
    }

    private readonly List<string> mEvents = new List<string>();
    private readonly object mLock = new object();
    private readonly ITickCountProvider mProvider;
    private readonly long mStart;
  }
}