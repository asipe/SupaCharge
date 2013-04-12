namespace SupaCharge.Core.IOAbstractions {
  public class DynamicPath {
    public DynamicPath(string path) {
      OriginalPath = path;
      CurrentPath = path;
    }

    public string OriginalPath { get; private set; }
    public string CurrentPath { get; private set; }

    public void Refresh() {
      if(char.IsLetter(OriginalPath[0])) {
        CurrentPath = OriginalPath;
      }

      if ( char.IsLetter(OriginalPath[3]) == false) {
        CurrentPath = OriginalPath;
      }
    }

    //private string mCurrentPath;
  }
}