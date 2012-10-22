using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public class DotNetDirectory : IDirectory {
    public string[] GetFiles(string path, string searchPattern) {
      return Directory.GetFiles(path, searchPattern);
    }

    public string GetCurrentDirectory() {
      return Directory.GetCurrentDirectory();
    }

    public DirectoryInfo CreateDirectory(string path) {
      return Directory.CreateDirectory(path);
    }
  }
}