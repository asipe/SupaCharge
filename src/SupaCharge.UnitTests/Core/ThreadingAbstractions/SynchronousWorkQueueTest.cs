using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class SynchronousWorkQueueTest : BaseTestCase {
    [Test]
    public void TestEnqueueWithWorkThatSetsFutureWithResult() {
      var future = mQueue.Enqueue(() => {
                                    CheckDifferentThread();
                                    return 55;
                                  });
      Assert.That(future.Wait(), Is.EqualTo(55));
    }

    [Test]
    public void TestEnqueueWithWorkThatThrowsFutureWithResult() {
      var future = mQueue.Enqueue(() => {
                                    CheckDifferentThread();
                                    throw new Exception("something bad");
                                  });
      var actualEx = Assert.Throws<FutureException>(future.Wait);
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException.Message, Is.EqualTo("something bad"));
    }

    [Test]
    public void TestEnqueueWithWorkWithDataThatSetsFutureWithResult() {
      var future = mQueue.Enqueue(2, x => {
                                       CheckDifferentThread();
                                       return 55 * x;
                                     });
      Assert.That(future.Wait(), Is.EqualTo(110));
    }

    [Test]
    public void TestEnqueueWithWorkWithDataThatThrowsFutureWithResult() {
      var future = mQueue.Enqueue<int, int>(2, x => {
                                                 CheckDifferentThread();
                                                 throw new Exception("something bad");
                                               });
      var actualEx = Assert.Throws<FutureException>(() => future.Wait());
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException.Message, Is.EqualTo("something bad"));
    }

    [Test]
    public void TestEnqueueWithWorkThatSetsFutureWithNoResult() {
      var ctr = 0;
      mQueue.Enqueue(() => {
                       CheckDifferentThread();
                       Interlocked.Increment(ref ctr);
                     }).Wait();
      var val = 1;
      Interlocked.CompareExchange(ref val, 0, ctr);
      Assert.That(val, Is.EqualTo(0));
    }

    [Test]
    public void TestEnqueueWithWorkThatThrowsFutureWithNoResult() {
      var future = mQueue.Enqueue(() => {
                                    CheckDifferentThread();
                                    throw new Exception("something bad");
                                  });
      var actualEx = Assert.Throws<FutureException>(future.Wait);
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException.Message, Is.EqualTo("something bad"));
    }

    [Test]
    public void TestEnqueueWithWorkWithDataThatSetsFutureWithNoResult() {
      var ctr = 0;
      mQueue.Enqueue(10, x => {
                           CheckDifferentThread();
                           Interlocked.Add(ref ctr, x);
                         }).Wait();
      var val = 10;
      Interlocked.CompareExchange(ref val, 0, ctr);
      Assert.That(val, Is.EqualTo(0));
    }

    [Test]
    public void TestEnqueueWithWorkWithDataThatThrowsFutureWithNoResult() {
      var future = mQueue.Enqueue(15, x => {
                                        CheckDifferentThread();
                                        throw new Exception("something bad " + x);
                                      });
      var actualEx = Assert.Throws<FutureException>(future.Wait);
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException.Message, Is.EqualTo("something bad 15"));
    }

    [Test]
    public void TestEnqueueMultipleItems() {
      var results = Enumerable
        .Range(0, 3)
        .Select(x => mQueue.Enqueue(() => x))
        .Select(f => f.Wait())
        .ToArray();

      Assert.That(results, Is.EqualTo(BA(0, 1, 2)));
    }

    [SetUp]
    public void DoSetup() {
      mQueue = new SynchronousWorkQueue();
      mThreadID = Thread.CurrentThread.ManagedThreadId;
    }

    private void CheckDifferentThread() {
      Assert.That(Thread.CurrentThread.ManagedThreadId, Is.EqualTo(mThreadID));
    }

    private SynchronousWorkQueue mQueue;
    private int mThreadID;
  }
}