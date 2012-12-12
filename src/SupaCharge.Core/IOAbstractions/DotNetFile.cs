using System.IO;

namespace SupaCharge.Core.IOAbstractions {
  public class DotNetFile : IFile {
    public FileStream Open(string path, FileMode mode) {
      return File.Open(path, mode);
    }

    public string ReadAllText(string path) {
      return File.ReadAllText(path);
    }

    public string[] ReadAllLines(string path) {
      return File.ReadAllLines(path);
    }

    public StreamReader OpenText(string path) {
      return File.OpenText(path);
    }

    public byte[] ReadAllBytes(string path) {
      return File.ReadAllBytes(path);
    }

    public void WriteAllBytes(string path, byte[] bytes) {
      File.WriteAllBytes(path, bytes);
    }

    public void WriteAllText(string path, string text) {
      File.WriteAllText(path, text);
    }

    public void Copy(string sourceFileName, string destFileName) {
      File.Copy(sourceFileName, destFileName);
    }

    public bool Exists(string path) {
      return File.Exists(path);
    }
  }
}