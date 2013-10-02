using System;

namespace SupaCharge.Core.Events {
  public static class EventExtensions {
    public static void RaiseEvent<T>(this EventHandler<T> evt, object sender, T args) where T : EventArgs {
      if (evt != null)
        evt(sender, args);
    }
  }
}
