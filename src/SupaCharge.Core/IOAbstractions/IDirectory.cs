namespace SupaCharge.Core.IOAbstractions {
  internal interface IDirectory {
    string[] GetFiles(string path, string searchPattern);
    string GetCurrentDirectory();
  }
}