using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public interface IDirectory {
    string[] GetFiles(string path, string searchPattern);
    string GetCurrentDirectory();
    DirectoryInfo CreateDirectory(string path);
    bool Exists(string path);
    string[] GetDirectories(string path);
    void Delete(string path);
    void Delete(string path, bool recursive);
    string[] GetFiles(string path, string searchPattern, SearchOption searchOption);
  }
}