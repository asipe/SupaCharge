using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.ExceptionHandling {
  public class ExceptionDump {
    public ExceptionDump(Exception exception) {
      mException = exception;
    }

    public override string ToString() {
      return FormatException(mException);
    }

    private static string FormatException(Exception exc) {
      var messages = new List<string>();

      while (exc != null) {
        if (messages.Count > 0)
          messages.Add("----- Inner Exception");
        messages.Add(exc.GetType().ToString());
        messages.Add(exc.Message);
        messages.Add(exc.StackTrace);
        exc = exc.InnerException;
      }

      return CombineMessages(messages);
    }

    private static string CombineMessages(IEnumerable<string> parts) {
      return string.Join(Environment.NewLine, parts.Where(s => IsValidMessage(s)).ToArray());
    }

    private static bool IsValidMessage(string message) {
      return !string.IsNullOrEmpty(message);
    }

    private readonly Exception mException;
  }
}