using System;
using System.Collections.Generic;
using System.Linq;
using SupaCharge.Core.Text.Extensions;

namespace SupaCharge.Core.Web.Utility {
  public class FormUrlEncodedDataParser {
    public IDictionary<string, object> Parse(string data) {
      return data.IsNullOrEmptyTrim()
               ? new Dictionary<string, object>()
               : ParseData(data);
    }

    private static IDictionary<string, object> ParseData(string data) {
      return data
        .Split('&')
        .Select(s => s.Split('='))
        .ToDictionary(s => s[0], s => (object)Uri.UnescapeDataString(s[1]));
    }
  }
}