using System;
using System.Linq;
using NUnit.Framework;
using SupaCharge.Core.Collections.Extensions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
  [TestFixture]
  public class ElementsAtExtensionTest : BaseTestCase {
    [TestCase(new int[0], new string[0])]
    [TestCase(new[] {0}, new[] {"a"})]
    [TestCase(new[] {0, 0}, new[] {"a", "a"})]
    [TestCase(new[] {0, 1, 2}, new[] {"a", "b", "c"})]
    [TestCase(new[] {0, 1, 0}, new[] {"a", "b", "a"})]
    [TestCase(new[] {0, 1, 0, 0, 1, 0, 1}, new[] {"a", "b", "a", "a", "b", "a", "b"})]
    public void TestElementsAt(int[] indexes, string[] expected) {
      Assert.That(new[] {"a", "b", "c"}.ElementsAt(indexes), Is.EqualTo(expected));
    }

    [Test]
    public void TestIfExceptionIsThrown2() {
      Assert.Throws<ArgumentOutOfRangeException>(() => new[] {"a", "b", "c"}.ElementsAt(150).ToArray());
    }
  }
}