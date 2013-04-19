using System;
using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public class DynamicPath {
    public DynamicPath(string path, IDns dotNetDns) {
      OriginalPath = CurrentPath = path;
      mDotNetDns = dotNetDns;
      mRootedPath = Path.IsPathRooted(path);
    }

    public void Refresh() {
      if (mRootedPath)
       DoRefresh();
    }

    private void DoRefresh() {
      mUri = new Uri(OriginalPath);

      if (mUri.HostNameType.ToString() == "Dns") {
        var ip = mDotNetDns.GetIPAddress(mUri.Host);
        CurrentPath = CurrentPath.Replace(mUri.Host, ip);
      }
    }

    public string OriginalPath { get; private set; }
    public string CurrentPath { get; private set; }

    private readonly IDns mDotNetDns;
    private readonly bool mRootedPath;
    private Uri mUri;
  }
}