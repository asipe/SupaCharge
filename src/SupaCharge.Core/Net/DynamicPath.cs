using System;
using System.IO;

namespace SupaCharge.Core.Net {
  public class DynamicPath {
    public DynamicPath(string path, IDns dotNetDns) {
      OriginalPath = CurrentPath = path;
      mDotNetDns = dotNetDns;
      mRootedPath = Path.IsPathRooted(path);
    }

    public string OriginalPath{get;private set;}
    public string CurrentPath{get;private set;}

    public void Refresh() {
      if (mRootedPath)
        DoRefresh();
    }

    private void DoRefresh() {
      var uri = new Uri(OriginalPath);

      if (uri.HostNameType.ToString() == "Dns")
        CurrentPath = OriginalPath.Replace(uri.Host, mDotNetDns.GetIPAddress(uri.Host));
    }

    private readonly IDns mDotNetDns;
    private readonly bool mRootedPath;
  }
}