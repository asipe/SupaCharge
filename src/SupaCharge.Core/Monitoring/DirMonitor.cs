using System;
using System.IO;

namespace SupaCharge.Core.Monitoring {
  public class DirMonitor {
    public DirMonitor(string pathToWatch) {
      mWatcher = new FileSystemWatcher(pathToWatch);
    }

    public void Start() {
      mWatcher.EnableRaisingEvents = true;
    }

    public void SetChangedFunctionality() {
      mWatcher.Changed += new FileSystemEventHandler(OnFileChange);
    }

    public void Stop() {
      mWatcher.EnableRaisingEvents = false;
    }

    public event EventHandler<FileSystemEventArgs> OnFileChange;
    private readonly FileSystemWatcher mWatcher;
  }
}