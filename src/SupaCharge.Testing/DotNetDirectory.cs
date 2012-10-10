using System.IO;

namespace SupaCharge.Testing {
  internal class DotNetDirectory : IDirectory {
    public string[] GetFiles(string path, string searchPattern) {
      return Directory.GetFiles(path, searchPattern);
    }

    public string GetCurrentDirectory() {
      return Directory.GetCurrentDirectory();
    }
  }
}