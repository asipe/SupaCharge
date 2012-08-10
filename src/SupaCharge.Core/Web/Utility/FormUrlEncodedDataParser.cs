using System;
using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.Web.Utility {
  public class FormUrlEncodedDataParser {
    public IDictionary<string, string> Parse(string data) {
      return IsNullOrEmptyTrim(data) 
               ? new Dictionary<string, string>()
               : ParseData(data);
    }

    private static bool IsNullOrEmptyTrim(string data) {
      return string.IsNullOrEmpty(data) || string.IsNullOrEmpty(data.Trim());
    }

    private static IDictionary<string, string> ParseData(string data) {
      return data
        .Split('&')
        .Select(s => s.Split('='))
        .ToDictionary(s => s[0], s => Uri.UnescapeDataString(s[1]));
    }
  }
}