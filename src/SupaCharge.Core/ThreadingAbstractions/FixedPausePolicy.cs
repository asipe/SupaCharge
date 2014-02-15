namespace SupaCharge.Core.ThreadingAbstractions {
  public class FixedPausePolicy : IRetryPausePolicy {
    public FixedPausePolicy(ISleep sleeper, int sleepMillis) {
      mSleeper = sleeper;
      mSleepMillis = sleepMillis;
    }

    public FixedPausePolicy(int sleepMillis) : this(new DotNetSleep(), sleepMillis) {
      mSleepMillis = sleepMillis;
    }

    public void Pause() {
      mSleeper.Sleep(mSleepMillis);
    }

    private readonly int mSleepMillis;
    private readonly ISleep mSleeper;
  }
}