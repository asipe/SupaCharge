using System;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class WorkQueueBatchTest : BaseTestCase {
    [Test]
    public void TestWaitWithNoItems() {
      mBatch.Wait(0);
    }

    [Test]
    public void TestAddSingleItemWithWait() {
      mBatch
        .Add(Inc)
        .Wait(0);
      Assert.That(mCount, Is.EqualTo(1));
    }

    [Test]
    public void TestAddMultipleItemsWithWait() {
      mBatch
        .Add(Inc)
        .Add(Inc)
        .Add(Inc, Inc)
        .Wait(0);
      Assert.That(mCount, Is.EqualTo(4));
    }

    [Test]
    public void TestAddMultipleItemsWithDataWithWait() {
      mBatch
        .Add(15, Add)
        .Add(50, Add)
        .Add(100, Add)
        .Wait(0);
      Assert.That(mCount, Is.EqualTo(165));
    }

    [Test]
    public void TestAddMixedItemsWithDataAndWithOutDataWithWait() {
      mBatch
        .Add(100, Add)
        .Add(Inc, Inc, Inc)
        .Wait(0)
        .Add(200, Add)
        .Add(Inc)
        .Wait(0);
      Assert.That(mCount, Is.EqualTo(304));
    }

    [Test]
    public void TestWaitAllWithNoItems() {
      mBatch.WaitAll(0);
    }

    [Test]
    public void TestWaitAllWithSingleItem() {
      mBatch
        .Add(100, Add)
        .WaitAll(0);
      Assert.That(mCount, Is.EqualTo(100));
    }

    [Test]
    public void TestWaitAllWithMultipleItems() {
      mBatch
        .Add(100, Add)
        .Add(Inc, Inc)
        .Add(5, Add)
        .WaitAll(0);
      Assert.That(mCount, Is.EqualTo(107));
    }

    [Test]
    public void TestWaitAllWithSingleFailing() {
      var ex = Assert.Throws<AggregatedException>(() => mBatch
                                                          .Add(() => {throw new Exception("test error 1");})
                                                          .WaitAll(0));
      Assert.That(ex.Message, Is.EqualTo("1 Activities Failed"));
      Assert.That(ex.Exceptions.Length, Is.EqualTo(1));
      Assert.That(mCount, Is.EqualTo(0));
    }

    [Test]
    public void TestWaitAllWithMixedFailingAndSuccess() {
      var ex = Assert.Throws<AggregatedException>(() => mBatch
                                                          .Add(() => {throw new Exception("test error 1");})
                                                          .Add(Inc, Inc, Inc)
                                                          .Add(() => {throw new Exception("test error 2");})
                                                          .Add(Inc, () => {throw new Exception("test error 2");})
                                                          .WaitAll(0));
      Assert.That(ex.Message, Is.EqualTo("3 Activities Failed"));
      Assert.That(ex.Exceptions.Length, Is.EqualTo(3));
      Assert.That(mCount, Is.EqualTo(4));
    }

    [SetUp]
    public void DoSetup() {
      mQueue = new SynchronousWorkQueue();
      mBatch = new WorkQueueBatch(mQueue);
      mCount = 0;
    }

    private void Inc() {
      ++mCount;
    }

    private void Add(int amt) {
      mCount += amt;
    }

    private SynchronousWorkQueue mQueue;
    private WorkQueueBatch mBatch;
    private int mCount;
  }
}