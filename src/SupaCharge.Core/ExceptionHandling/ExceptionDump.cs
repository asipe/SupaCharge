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
      return mError.Message;
    }

    private Exception mError;
  }
}
