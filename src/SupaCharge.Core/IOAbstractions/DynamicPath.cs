using System;

namespace SupaCharge.Core.IOAbstractions {
  public class DynamicPath {
    public DynamicPath(string path, IDns dotNetDns) {
      OriginalPath = path;
      CurrentPath = path;
      mDotNetDns = dotNetDns;
      mUri = new Uri(path);
    }

    public string OriginalPath { get; private set; }
    public string CurrentPath { get; private set; }

    public void Refresh() {
      if (char.IsLetter(OriginalPath[0])) CurrentPath = OriginalPath;

      else if (char.IsDigit(OriginalPath[3])) CurrentPath = OriginalPath;

      else if (OriginalPath[0] == '\\' && char.IsLetter(OriginalPath[3])) CurrentPath = mDotNetDns.GetIPAddress(FindHostName());
    }

    private string FindHostName() {
      var x = 2;

      while (true)
        if (char.IsLetterOrDigit(OriginalPath[x])) x++;

        else return OriginalPath.Substring(2, x - 2);
    }

    private Uri mUri;
    private readonly IDns mDotNetDns;
  }
}