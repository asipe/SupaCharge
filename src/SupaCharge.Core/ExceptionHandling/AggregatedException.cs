using System;
using System.Runtime.Serialization;
using System.Text;

namespace SupaCharge.Core.ExceptionHandling {
  [Serializable]
  public class AggregatedException : Exception {
    public AggregatedException(Exception[] exceptions) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, SerializationInfo info, StreamingContext context) : base(info, context) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, string msg) : base(msg) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, string msg, Exception e) : base(msg, e) {
      Exceptions = exceptions;
    }

    public AggregatedException(Exception[] exceptions, string msg, params object[] fmt) : base(string.Format(msg, fmt)) {
      Exceptions = exceptions;
    }

    public Exception[] Exceptions{get;private set;}

    public override string ToString() {
      var result = new StringBuilder();
      result.AppendLine(base.ToString());

      foreach (var exception in Exceptions) {
        result.AppendLine();
        result.AppendLine();
        result.AppendLine(exception.ToString());
      }

      return result.ToString();
    }
  }
}