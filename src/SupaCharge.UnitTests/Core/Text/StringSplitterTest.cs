using System.Collections.Generic;
using NUnit.Framework;
using SupaCharge.Core.Text;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Text {
  [TestFixture]
  public class StringSplitterTest : BaseTestCase {
    [TestCaseSource("GetStringSplitterTests")]
    public void TestUsage(int lookback, int maxLength, bool trimResultLines, string input, string[] expected) {
      var actual = new StringSplitter(lookback, maxLength, trimResultLines).Split(input);
      Assert.That(actual, Is.EqualTo(expected));
    }

    private IEnumerable<object> GetStringSplitterTests() {
      yield return new TestCaseData(3, 5, false, "a", BA("a"));
      yield return new TestCaseData(3, 5, false, "a ", BA("a "));
      yield return new TestCaseData(3, 5, false, "aa", BA("aa"));
      yield return new TestCaseData(3, 5, false, "a a", BA("a a"));
      yield return new TestCaseData(3, 5, false, "a a ", BA("a a "));
      yield return new TestCaseData(3, 5, false, "aaa", BA("aaa"));
      yield return new TestCaseData(3, 5, false, "aaaa", BA("aaaa"));
      yield return new TestCaseData(3, 5, false, "aa aa", BA("aa aa"));
      yield return new TestCaseData(3, 5, false, "aaaaa", BA("aaaaa"));
      yield return new TestCaseData(3, 5, false, "aaaaab", BA("aaaaa", "b"));
      yield return new TestCaseData(3, 5, false, "aaaaabb", BA("aaaaa", "bb"));
      yield return new TestCaseData(3, 5, false, "aaaaabbbbb", BA("aaaaa", "bbbbb"));
      yield return new TestCaseData(3, 5, false, "aaaaabbbbbccccc", BA("aaaaa", "bbbbb", "ccccc"));
      yield return new TestCaseData(3, 5, false, "aaaaabbbbbcccccd", BA("aaaaa", "bbbbb", "ccccc", "d"));
      yield return new TestCaseData(3, 5, false, "aa aabbbbbccccc", BA("aa ", "aabbb", "bbccc", "cc"));
      yield return new TestCaseData(3, 5, false, "aa aabb bbcc cc", BA("aa ", "aabb ", "bbcc ", "cc"));
      yield return new TestCaseData(3, 5, false, "aa aa bb bbcc cc", BA("aa ", "aa ", "bb ", "bbcc ", "cc"));
      yield return new TestCaseData(10, 25, false, "now is the time for all good men to come to the aid of their country", BA("now is the time for all ", "good men to come to the ", "aid of their country"));
      yield return new TestCaseData(10, 20, false, "now is the time for all good men to come to the aid of their country", BA("now is the time for ", "all good men to ", "come to the aid of ", "their country"));
      yield return new TestCaseData(2, 20, false, "now is the time for all good men to come to the aid of their country", BA("now is the time for ", "all good men to come", " to the aid of their", " country"));

      yield return new TestCaseData(3, 5, true, "a", BA("a"));
      yield return new TestCaseData(3, 5, true, "a ", BA("a"));
      yield return new TestCaseData(3, 5, true, "aa", BA("aa"));
      yield return new TestCaseData(3, 5, true, "a a", BA("a a"));
      yield return new TestCaseData(3, 5, true, "a a ", BA("a a"));
      yield return new TestCaseData(3, 5, true, "aaa", BA("aaa"));
      yield return new TestCaseData(3, 5, true, "aaaa", BA("aaaa"));
      yield return new TestCaseData(3, 5, true, "aa aa", BA("aa aa"));
      yield return new TestCaseData(3, 5, true, "aaaaa", BA("aaaaa"));
      yield return new TestCaseData(3, 5, true, "aaaaab", BA("aaaaa", "b"));
      yield return new TestCaseData(3, 5, true, "aaaaabb", BA("aaaaa", "bb"));
      yield return new TestCaseData(3, 5, true, "aaaaabbbbb", BA("aaaaa", "bbbbb"));
      yield return new TestCaseData(3, 5, true, "aaaaabbbbbccccc", BA("aaaaa", "bbbbb", "ccccc"));
      yield return new TestCaseData(3, 5, true, "aaaaabbbbbcccccd", BA("aaaaa", "bbbbb", "ccccc", "d"));
      yield return new TestCaseData(3, 5, true, "aa aabbbbbccccc", BA("aa", "aabbb", "bbccc", "cc"));
      yield return new TestCaseData(3, 5, true, "aa aabb bbcc cc", BA("aa", "aabb", "bbcc", "cc"));
      yield return new TestCaseData(3, 5, true, "aa aa bb bbcc cc", BA("aa", "aa", "bb", "bbcc", "cc"));
      yield return new TestCaseData(10, 25, true, "now is the time for all good men to come to the aid of their country", BA("now is the time for all", "good men to come to the", "aid of their country"));
      yield return new TestCaseData(10, 20, true, "now is the time for all good men to come to the aid of their country", BA("now is the time for", "all good men to", "come to the aid of", "their country"));
      yield return new TestCaseData(2, 20, true, "now is the time for all good men to come to the aid of their country", BA("now is the time for", "all good men to come", "to the aid of their", "country"));
    }
  }
}