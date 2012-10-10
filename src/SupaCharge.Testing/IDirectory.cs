using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupaCharge.Testing {
  interface IDirectory {
    string[] GetFiles(string path, string searchPattern);
    string GetCurrentDirectory();
  }
}
