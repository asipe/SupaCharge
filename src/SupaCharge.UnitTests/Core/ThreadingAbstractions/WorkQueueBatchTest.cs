using System;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class WorkQueueBatchTest : BaseTestCase {
    [Test]
    public void TestDefaultHas0FutureCount() {
      Assert.That(mBatch.PendingFutureCount, Is.EqualTo(0));
    }

    [Test]
    public void TestWaitAllWithNoItems() {
      mBatch.WaitAll(0);
    }

    [Test]
    public void TestPendingFutureCountWithUnresolvedFutures() {
      mBatch.Add(100, Add);
      Assert.That(mBatch.PendingFutureCount, Is.EqualTo(1));
      mBatch.Add(100, Add);
      Assert.That(mBatch.PendingFutureCount, Is.EqualTo(2));
      mBatch.WaitAll(0);
      Assert.That(mBatch.PendingFutureCount, Is.EqualTo(0));
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
      Assert.That(ex.Message, Is.StringStarting("1 Activities Failed"));
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
      Assert.That(ex.Message, Is.StringStarting("3 Activities Failed"));
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