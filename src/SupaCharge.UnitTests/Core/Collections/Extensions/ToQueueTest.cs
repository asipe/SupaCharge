using System;
using NUnit.Framework;
using SupaCharge.Core.Collections.Extensions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
  [TestFixture]
  public class ToQueueExtensionTest : BaseTestCase {
    [Test]
    public void TestQueueHasCorrectElements() {
      var arry = new[] {1, 2, 3};
      var queue = arry.ToQueue();

      Assert.That(queue.Dequeue(), Is.EqualTo(1));
      Assert.That(queue.Dequeue(), Is.EqualTo(2));
      Assert.That(queue.Dequeue(), Is.EqualTo(3));
      Assert.That(queue.Count, Is.EqualTo(0));
    }

    [Test]
    public void TestQueueReturnsAnEmptyQueue() {
      var arry = new int[] {};
      var queue = arry.ToQueue();

      Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    [Test]
    public void TestQueueReturnsASingelElementQueue() {
      var arry = new[] {1};
      var queue = arry.ToQueue();

      Assert.That(queue.Dequeue(), Is.EqualTo(1));
    }
  }
}