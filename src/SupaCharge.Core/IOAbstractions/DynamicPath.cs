using System;
using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public class DynamicPath {
   public DynamicPath(string path, IDns dotNetDns) {
     OriginalPath = path;
     CurrentPath = path;
     mDotNetDns = dotNetDns;
     mWellFormed = false;
     mPath = path;
     if (Path.IsPathRooted(path))
       mWellFormed = true;
   }

    public string OriginalPath { get; private set; }
    public string CurrentPath { get; private set; }

    public void Refresh() {
      if (mWellFormed) {
        mUri = new Uri(mPath);
        if (mUri.HostNameType.ToString() == "Dns" && mWellFormed)
          CurrentPath = mDotNetDns.GetIPAddress(mUri.Host);
      }
    }

    private readonly IDns mDotNetDns;
    private Uri mUri;
    private bool mWellFormed;
    private string mPath;
  }
}