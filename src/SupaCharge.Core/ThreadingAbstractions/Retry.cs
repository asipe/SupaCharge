using System;
using System.Threading;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class Retry {
    public Retry(int numberOfAttempts, int millisBetweenAttempts) {
      mNumberOfAttempts = numberOfAttempts;
      mMillisBetweenAttempts = millisBetweenAttempts;
    }

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
          Thread.Sleep(mMillisBetweenAttempts);
        }
    }

    private readonly int mMillisBetweenAttempts;
    private readonly int mNumberOfAttempts;
    private Action<Exception> mErrorHandler = ex => {throw ex;};
    private Action<int> mWork;
  }
}