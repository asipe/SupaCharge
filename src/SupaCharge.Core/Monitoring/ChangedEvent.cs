using System;

namespace SupaCharge.Core.Monitoring {
  public class ChangedEvent : EventArgs {
    public ChangedEvent(string fileName) {
      FileName = fileName;
    }

    public string FileName{get;set;}
  }
}