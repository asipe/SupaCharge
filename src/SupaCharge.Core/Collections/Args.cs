namespace SupaCharge.Core.Collections {
  public class Args {
    public Args(params object[] args) {
      mArgs = args;
    }

    public T Get<T>(int index) {
      return (T)mArgs[index];
    }

    private readonly object[] mArgs;
  }
}