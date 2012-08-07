using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupaCharge.Core.ExceptionHandling {
  public class ExceptionDump {
    public ExceptionDump(Exception error) {
      mError = error;
      ToString();
    }

    public override string ToString() {
      var parts = new List<string>();
      parts.Add(mError.GetType().ToString());
      parts.Add(mError.Message);
      parts.Add(mError.StackTrace);

      var result = parts
        .Where(s => s != null)
        .ToArray();

      return string.Join(Environment.NewLine, result);
    }

    private string GetStack() {
      return mError.StackTrace;
    }

    private Exception mError;
  }
}
