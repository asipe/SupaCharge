using System.Collections.Generic;

namespace SupaCharge.Core.DiagnosticAbstractions {
  public class RecordingEventList : IRecordingEventList {
    public RecordingEventList(ITickCountProvider provider) {
      mProvider = provider;
      AddMessage(string.Format("Frequency: {0}", mProvider.GetFrequency()));
      AddMessage("----------");
      AddStampedEvent(0, "Created");
      mStart = mProvider.GetTicks();
    }

    public void AddEvent(string msg, params object[] args) {
      AddStampedEvent(mProvider.GetTicks() - mStart, msg, args);
    }

    public string[] GetEvents() {
      lock (mLock) {
        return mEvents.ToArray();
      }
    }

    private void AddStampedEvent(long ticks, string msg, params object[] args) {
      AddMessage(string.Format("{0}: {1}", ticks, string.Format(msg, args)));
    }

    private void AddMessage(string msg) {
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