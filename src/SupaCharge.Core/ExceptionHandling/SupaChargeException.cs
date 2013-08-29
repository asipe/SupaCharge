using System;
using System.Runtime.Serialization;

namespace SupaCharge.Core.ExceptionHandling {
  [Serializable]
  public class SupaChargeException : Exception {
    public SupaChargeException() {}
    public SupaChargeException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    public SupaChargeException(string msg) : base(msg) {}
    public SupaChargeException(string msg, Exception e) : base(msg, e) {}
    public SupaChargeException(string msg, params object[] fmt) : base(string.Format(msg, fmt)) {}
  }
}