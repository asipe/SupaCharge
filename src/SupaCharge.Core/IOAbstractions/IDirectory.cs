namespace SupaCharge.Core.IOAbstractions {
  interface IDirectory {
    string[] GetFiles(string path, string searchPattern);
    string GetCurrentDirectory();
  }
}
