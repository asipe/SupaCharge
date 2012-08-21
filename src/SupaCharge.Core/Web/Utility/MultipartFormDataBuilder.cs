using System.Collections.Generic;
using System.Linq;

namespace SupaCharge.Core.Web.Utility {
  public class MultipartFormDataBuilder {
    private class Entry {
      public string Key{get;set;}
      public object Value{get;set;}
    }

    public MultipartFormDataBuilder(string boundary) {
      mBoundary = boundary;
    }

    public void Add(string key, object value) {
      mEntries.Add(new Entry {Key = key, Value = value});
    }

    public string Build() {
      return mEntries.Any() ? BuildData() : "";
    }

    private string BuildData() {
      var bodyParts = mEntries.Select(e => string.Concat(_BodyParts[0], mBoundary, _BodyParts[1], e.Key, _BodyParts[2], e.Value)).ToArray();
      return _Header + string.Join(_Sep, bodyParts) + GetFooter();
    }

    private string GetFooter() {
      return string.Concat(_Sep, "--", mBoundary, "--");
    }

    private const string _Sep = "\r\n";
    private const string _Header = "\r\n\r\n";

    private static readonly string[] _BodyParts = new[] {
                                                          "--",
                                                          "\r\ncontent-disposition: form-data; name=\"",
                                                          "\"\r\n\r\n"
                                                        };

    private readonly string mBoundary;
    private readonly List<Entry> mEntries = new List<Entry>();
  }
}