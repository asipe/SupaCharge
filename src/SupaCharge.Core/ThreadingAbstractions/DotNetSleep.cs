using System.Threading;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class DotNetSleep : ISleep {
    public void Sleep(int millisecondsTimeOut) {
      Thread.Sleep(millisecondsTimeOut);
    }
  }
}