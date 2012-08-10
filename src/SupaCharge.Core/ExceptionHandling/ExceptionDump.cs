using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.ExceptionHandling {
  public class ExceptionDump {
    public ExceptionDump(Exception Exception) {
      mException = Exception;
      mIsInnerException = false;
      if (HasInnerException(Exception))
        mIsInnerException = true;
    }

    public override string ToString() {
      var parts = FormatException(mException);
      return parts;
    }

    private static bool HasInnerException(Exception exc) {
      return exc.InnerException != null;
    }

    private string FormatException(Exception exc) {
      var parts = new List<string>();

      while (exc != null) {
        if (mIsInnerException && exc != mException)
          parts.Add("----- Inner Exception");

        parts.Add(exc.GetType().ToString());
        parts.Add(exc.Message);
        parts.Add(exc.StackTrace);

        exc = exc.InnerException;
      }

      var result = parts
        .Where(s => s != null && FilterEmptyItem(s))
        .ToArray();

      return string.Join(Environment.NewLine, result);
    }

    private static bool FilterEmptyItem(string item) {
      return (item.Length > 0 && item != string.Empty);
    }

    private readonly bool mIsInnerException;
    private readonly Exception mException;
  }
}