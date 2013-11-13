using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SupaCharge.Core.ExceptionHandling {
  [Serializable]
  public class AggregatedException : Exception {
    public AggregatedException(Exception[] exceptions) : base(BuildMessage(null, exceptions)) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, SerializationInfo info, StreamingContext context) : base(info, context) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, string msg) : base(BuildMessage(msg, exceptions)) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, string msg, Exception e) : base(BuildMessage(msg, exceptions), e) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, string msg, params object[] fmt) : base(BuildMessage(string.Format(msg, fmt), exceptions)) {
      Exceptions = exceptions;
    }

    public Exception[] Exceptions{get;private set;}

    private static string BuildMessage(string msg, IEnumerable<Exception> exceptions) {
      var result = new StringBuilder();

      if (msg != null)
        result.AppendLine(msg);

      foreach (var exception in exceptions) {
        result.AppendLine("-------------------------------------");
        result.AppendLine();
        result.AppendLine();
        result.AppendLine(exception.ToString());
        result.AppendLine("-------------------------------------");
      }

      return result.ToString();
    }
  }
}