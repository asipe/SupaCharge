using System;
using System.IO;
using SupaCharge.Core.Events;

namespace SupaCharge.Core.Monitoring {
  public class DirMonitor {
    public DirMonitor(string pathToWatch) {
      mWatcher = new FileSystemWatcher(pathToWatch);
    }

    public void Start() {
      mWatcher.Changed += WatcherOnChanged;
      mWatcher.EnableRaisingEvents = true;
    }

    private void WatcherOnChanged(object sender, FileSystemEventArgs e) {
      OnFileChange.RaiseEvent(null, new ChangedEvent(e.Name));
    }

    public void Stop() {
      mWatcher.EnableRaisingEvents = false;
    }

    public event EventHandler<ChangedEvent> OnFileChange;
    private readonly FileSystemWatcher mWatcher;
  }
}