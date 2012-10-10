using System;
using System.Linq;
using NUnit.Framework;
using SupaCharge.Core.Collections.Extensions;
using System.Collections.Generic;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
  [TestFixture]
  public class ToQueue: BaseTestCase {
    [Test]
    public void TestQueueHasCorrectElements() {
      var arry = new int[] { 1, 2, 3 };
      var queue = arry.ToQueue();

      Assert.That(queue.Dequeue(), Is.EqualTo(1));
      Assert.That(queue.Dequeue(), Is.EqualTo(2));
      Assert.That(queue.Dequeue(), Is.EqualTo(3));
    }

    [Test]
    public void TestQueueReturnsAnEmptyQueue() {
      var arry = new int[] {};
      var queue = arry.ToQueue();

      Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    [Test]
    public void TestQueueReturnsASingelElementQueue() {
      var arry = new int[] {1};
      var queue = arry.ToQueue();

      Assert.That(queue.Dequeue(), Is.EqualTo(1));
    }
  }
}