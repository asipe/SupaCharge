using System;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class Retry {
    public Retry(int numberOfAttempts, IRetryPausePolicy pausePolicy) {
      mNumberOfAttempts = numberOfAttempts;
      mPausePolicy = pausePolicy;
    }

    public Retry(int numberOfAttempts, int millisBetweenAttempts) : this(numberOfAttempts, new FixedPausePolicy(millisBetweenAttempts)) {}

    public Retry WithWork(Action<int> work) {
      mWork = work;
      return this;
    }

    public Retry WithErrorHandler(Action<Exception> handler) {
      mErrorHandler = handler;
      return this;
    }

    public void Start() {
      var iteration = 0;
      while (true)
        try {
          mWork(iteration++);
          break;
        } catch (Exception ex) {
          if (iteration >= mNumberOfAttempts) {
            mErrorHandler(ex);
            break;
          }
          mPausePolicy.Pause();
        }
    }

    private readonly int mNumberOfAttempts;
    private readonly IRetryPausePolicy mPausePolicy;
    private Action<Exception> mErrorHandler = ex => {throw ex;};
    private Action<int> mWork;
  }
}