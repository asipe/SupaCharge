using NUnit.Framework;
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