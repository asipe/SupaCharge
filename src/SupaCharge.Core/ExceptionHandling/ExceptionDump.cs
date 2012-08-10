using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.ExceptionHandling {
  public class ExceptionDump {
    public ExceptionDump(Exception Exception) {
      mException = Exception;
      IsInnerException = false;
      if (HasInnerException(Exception))
        IsInnerException = true;
    }

    private bool HasInnerException(Exception exc) {
      return exc.InnerException != null;
    }

    private string FormatException(Exception exc) {
      var parts = new List<string>();

      while (exc != null) {
        if (IsInnerException && exc != mException)
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

    private bool FilterEmptyItem(string item) {
      return (item.Length > 0 && item != string.Empty);
    }

    public override string ToString() {
      var parts = FormatException(mException);
      return parts;
    }

    private readonly bool IsInnerException;
    private readonly Exception mException;
  }
}