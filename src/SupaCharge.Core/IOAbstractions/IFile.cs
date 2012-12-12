using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public interface IFile {
    FileStream Open(string path, FileMode mode);
    string[] ReadAllLines(string path);
  }
}