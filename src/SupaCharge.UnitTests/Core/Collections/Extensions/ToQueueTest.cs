using NUnit.Framework;
using SupaCharge.Core.Collections.Extensions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
  [TestFixture]
  public class ToQueueExtensionTest : BaseTestCase {
    [TestCase(new int[0])]
    [TestCase(new[] {1})]
    [TestCase(new[] {1, 2, 3})]
    public void TestToQueueExtension(int[] initial) {
      var queue = initial.ToQueue();
      foreach (var x in initial)
        Assert.That(queue.Dequeue(), Is.EqualTo(x));
      Assert.That(queue, Is.Empty);
    }
  }
}