namespace SupaCharge.Core.ThreadingAbstractions {
  public class IncreasingPausePolicy : IRetryPausePolicy {
    public IncreasingPausePolicy(ISleep sleeper, int startingSleepMillis, int percentageToIncrease) {
      mSleeper = sleeper;
      mCurrentSleepMillis = startingSleepMillis;
      mPercentageToIncreaseAsDecimal = percentageToIncrease / 100m;
    }

    public IncreasingPausePolicy(int startingSleepMillis, int percentageToIncreaseAsDecimal) : this(new DotNetSleep(), startingSleepMillis, percentageToIncreaseAsDecimal) {}

    public void Pause() {
      mSleeper.Sleep(mCurrentSleepMillis);
      mCurrentSleepMillis = (int)(mCurrentSleepMillis * (1 + mPercentageToIncreaseAsDecimal));
    }

    private readonly decimal mPercentageToIncreaseAsDecimal;
    private readonly ISleep mSleeper;
    private int mCurrentSleepMillis;
  }
}