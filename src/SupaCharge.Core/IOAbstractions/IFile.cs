using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public interface IFile {
    FileStream Open(string path, FileMode mode);
    FileStream Open(string path, FileMode mode, FileAccess access);
    string ReadAllText(string path);
    string[] ReadAllLines(string path);
    StreamReader OpenText(string path);
    byte[] ReadAllBytes(string path);
    void WriteAllBytes(string path, byte[] bytes);
    void WriteAllText(string path, string text);
    void WriteAllLines(string path, params string[] contents);
    void Copy(string sourceFileName, string destFileName);
    bool Exists(string path);
    void Delete(string path);
    long GetSize(string path);
  }
}