using System.Diagnostics;
using System.IO;
using System.Threading;
using SupaCharge.Core.ExceptionHandling;

namespace SupaCharge.Core.IOAbstractions {
  public class DotNetFile : IFile {
    public Stream Open(string path, FileMode mode) {
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

    public void WriteAllLines(string path, params string[] contents) {
      File.WriteAllLines(path, contents);
    }

    public void Copy(string sourceFileName, string destFileName) {
      File.Copy(sourceFileName, destFileName);
    }

    public void Copy(string sourceFileName, string destFileName, bool overwrite) {
      File.Copy(sourceFileName, destFileName, overwrite);
    }

    public bool Exists(string path) {
      return File.Exists(path);
    }

    public void Delete(string path) {
      File.Delete(path);
    }

    public void Delete(string path, int waitMilliseconds) {
      Delete(path);

      if (!Exists(path))
        return;

      var watch = Stopwatch.StartNew();
      while (true)
        if (!Exists(path))
          return;
        else if ((watch.ElapsedMilliseconds < waitMilliseconds))
          Thread.Sleep(15);
        else
          break;
      throw new SupaChargeException("Unable to verify delete of {0} in {1}ms", path, waitMilliseconds);
    }

    public long GetSize(string path) {
      return new FileInfo(path).Length;
    }

    public void Move(string sourceFileName, string destFileName) {
      File.Move(sourceFileName, destFileName);
    }

    public Stream Open(string path, FileMode mode, FileAccess access) {
      return File.Open(path, mode, access);
    }
  }
}