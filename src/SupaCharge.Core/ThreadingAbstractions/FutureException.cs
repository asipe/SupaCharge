using System;
using System.Runtime.Serialization;
using SupaCharge.Core.ExceptionHandling;

namespace SupaCharge.Core.ThreadingAbstractions {
  public class FutureException : SupaChargeException {
    public FutureException() { }
    public FutureException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public FutureException(string msg) : base(msg) { }
    public FutureException(string msg, Exception e) : base(msg, e) { }
    public FutureException(string msg, params object[] fmt) : base(string.Format(msg, fmt)) { }
  }
}