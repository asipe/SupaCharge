using System.Collections.Generic;

namespace SupaCharge.Core.Text {
  public class StringSplitter : IStringSplitter {
    public StringSplitter(int lookback, int maxLength, bool trimResultLines) {
      mLookback = lookback;
      mMaxLength = maxLength;
      mTrimResultLines = trimResultLines;
    }

    public string[] Split(string value) {
      var result = new List<string>();

      do {
        var pos = GetPosition(value);
        result.Add(Prepare(value.Substring(0, pos)));
        value = value.Substring(pos);
      } while (value.Length > 0);

      return result.ToArray();
    }

    private int GetPosition(string value) {
      return (value.Length <= mMaxLength)
               ? value.Length
               : FindFirstSplitPosition(value);
    }

    private int FindFirstSplitPosition(string value) {
      var pos = value.LastIndexOf(' ', mMaxLength - 1, mLookback);
      return (pos == -1)
               ? mMaxLength
               : pos + 1;
    }

    private string Prepare(string value) {
      return mTrimResultLines
               ? value.Trim()
               : value;
    }

    private readonly int mLookback;
    private readonly int mMaxLength;
    private readonly bool mTrimResultLines;
  }
}