using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public interface IFile {
    FileStream Open(string path, FileMode mode);
    string ReadAllText(string path);
    StreamReader OpenText(string path);
    byte[] ReadAllBytes(string path);
    void WriteAllBytes(string path, byte[] bytes);
    void WriteAllText(string path, string text);
    void Copy(string sourceFileName, string destFileName);
  }
}