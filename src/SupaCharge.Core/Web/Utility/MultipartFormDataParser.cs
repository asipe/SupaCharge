using System;
using System.Collections.Generic;
using System.Linq;
using SupaCharge.Core.Text.Extensions;

namespace SupaCharge.Core.Web.Utility {
  public class MultipartFormDataParser {
    public IDictionary<string, object> Parse(string contentType, string data) {
      return data.IsNullOrEmptyTrim()
               ? new Dictionary<string, object>()
               : ParseData(GetBoundary(contentType), data);
    }

    private static string GetBoundary(string contentType) {
      return new string(contentType
                          .Split(new[] {"boundary="}, StringSplitOptions.None)[1]
                          .TakeWhile(c => c != ';')
                          .ToArray());
    }

    private static IDictionary<string, object> ParseData(string boundary, string data) {
      boundary = "--" + boundary;

      var results = new Dictionary<string, object>();
      var bodies = data.Split(new[] {boundary + "\r\n", boundary + "--\r\n"}, StringSplitOptions.None);

      foreach (var body in bodies) {
        if (body.Trim().Length == 0)
          continue;

        if (body == "--")
          continue;

        var idx = body.IndexOf("name=\"", StringComparison.Ordinal);
        var key = new string(body.Skip(idx + 6).TakeWhile(c => c != '"').ToArray());

        var parts = body
          .Split(new[] {"\r\n"}, StringSplitOptions.None)
          .SkipWhile(line => line.StartsWith("content", StringComparison.OrdinalIgnoreCase))
          .SkipWhile(line => line.Length == 0)
          .ToArray();

        var value = string.Join("\r\n", parts.Take(parts.Length - 1).ToArray());

        if (key.Length > 0)
          results.Add(key, value);
      }

      return results;
    }
  }
}