using NUnit.Framework;
using SupaCharge.Core.Text.Extensions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
  [TestFixture]
  public class IsNullOrEmptyTrimExtensionTest : BaseTestCase {
    [TestCase("", true)]
    [TestCase(" ", true)]
    [TestCase(null, true)]
    [TestCase("hello", false)]
    [TestCase("  hello     ", false)]
    public void TestExtensionMethod(string value, bool expected) {
      Assert.That(value.IsNullOrEmptyTrim(), Is.EqualTo(expected));
    }
  }
}