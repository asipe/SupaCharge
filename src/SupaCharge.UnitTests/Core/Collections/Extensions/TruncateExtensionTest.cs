using NUnit.Framework;
using SupaCharge.Core.Collections.Extensions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
  [TestFixture]
  public class TruncateExtensionTest : BaseTestCase {
    [TestCase(new[] {1, 2, 3, 4}, 0, new[] {1, 2, 3, 4})]
    [TestCase(new[] {1, 2, 3, 4}, 1, new[] {1, 2, 3})]
    [TestCase(new[] {1, 2, 3, 4}, 2, new[] {1, 2})]
    [TestCase(new[] {1, 2, 3, 4}, 4, new int[0])]
    [TestCase(new[] {1, 2, 3, 4}, 400, new int[0])]
    [TestCase(new[] {1}, 1, new int[0])]
    [TestCase(new int[0], 1, new int[0])]
    [TestCase(new int[0], 10, new int[0])]
    public void TestExtensionMethod(int[] initial, int truncateNumber, int[] expected) {
      Assert.That(initial.Truncate(truncateNumber), Is.EqualTo(expected));
    }
  }
}