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

    public bool Exists(string path) {
      return Directory.Exists(path);
    }

    public string[] GetDirectories(string path) {
      return Directory.GetDirectories(path);
    }

    public void Delete(string path) {
      Directory.Delete(path);
    }

    public void Delete(string path, bool recursive) {
      Directory.Delete(path, recursive);
    }
  }
}