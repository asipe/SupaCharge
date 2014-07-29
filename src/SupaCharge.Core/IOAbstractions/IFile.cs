using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public interface IFile {
    Stream Open(string path, FileMode mode);
    Stream Open(string path, FileMode mode, FileAccess access);
    Stream Open(string path, FileMode mode, FileAccess access, FileShare share);
    string ReadAllText(string path);
    string ReadAllTextTrimmed(string path);
    string[] ReadAllLines(string path);
    StreamReader OpenText(string path);
    byte[] ReadAllBytes(string path);
    void WriteAllBytes(string path, byte[] bytes);
    void WriteAllText(string path, string text);
    void WriteAllLines(string path, params string[] contents);
    void Copy(string sourceFileName, string destFileName);
    void Copy(string sourceFileName, string destFileName, bool overwrite);
    bool Exists(string path);
    void Delete(string path);
    void Delete(string path, int waitMilliseconds);
    long GetSize(string path);
    void Move(string sourceFileName, string destFileName);
  }
}