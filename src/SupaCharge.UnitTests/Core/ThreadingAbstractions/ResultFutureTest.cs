using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class ResultFutureTest : BaseTestCase {
    [Test]
    public void TestWaitInfinitWithValueAlreadySetGivesReturn() {
      mFuture.Set(10);
      Assert.That(TimeThis(() => mFuture.Wait()), Is.LessThan(100));
    }

    [Test]
    public void TestWaitWithTimeoutWithValueAlreadySetGivesReturn() {
      mFuture.Set(10);
      Assert.That(TimeThis(() => mFuture.Wait(0)), Is.LessThan(100));
    }

    [Test]
    public void TestWaitInfinitWithValueNotYetSetGivesReturnWhenSet() {
      SetValueIn(50);
      Assert.That(TimeThis(() => mFuture.Wait()), Is.LessThan(150));
    }

    [Test]
    public void TestWaitWithTimeoutWithValueNotYetSetGivesReturnWhenSet() {
      SetValueIn(50);
      Assert.That(TimeThis(() => mFuture.Wait(500)), Is.LessThan(150));
    }

    [Test]
    public void TestWaitWithTimeoutWithValueNeverSetThrowsTimeoutException() {
      var ex = Assert.Throws<TimeoutException>(() => mFuture.Wait(0));
      Assert.That(ex.Message, Is.EqualTo("Timeout waiting for future to resolve"));
    }

    [Test]
    public void TestWaitInfiniteWithFailedAlreadySetThrows() {
      var originalEx = new Exception("something bad");
      mFuture.Failed(originalEx);
      var actualEx = Assert.Throws<FutureException>(() => mFuture.Wait());
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException, Is.EqualTo(originalEx));
    }

    [Test]
    public void TestWaitTimeoutWithFailedAlreadySetThrows() {
      var originalEx = new Exception("something bad");
      mFuture.Failed(originalEx);
      var actualEx = Assert.Throws<FutureException>(() => mFuture.Wait(0));
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException, Is.EqualTo(originalEx));
    }

    [Test]
    public void TestWaitInfiniteWithNothingSetThrowsWhenFailedSet() {
      var originalEx = new Exception("something bad");
      SetFailedIn(50, originalEx);
      var actualEx = Assert.Throws<FutureException>(() => mFuture.Wait());
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException, Is.EqualTo(originalEx));
    }

    [Test]
    public void TestWaitTimeoutWithNothingSetThrowsWhenFailedSet() {
      var originalEx = new Exception("something bad");
      SetFailedIn(50, originalEx);
      var actualEx = Assert.Throws<FutureException>(() => mFuture.Wait(500));
      Assert.That(actualEx.Message, Is.EqualTo("Error Resolving Future"));
      Assert.That(actualEx.InnerException, Is.EqualTo(originalEx));
    }

    [SetUp]
    public void DoSetup() {
      mFuture = new ResultFuture<int>();
    }

    private static long TimeThis(Action work) {
      var sw = Stopwatch.StartNew();
      work();
      return sw.ElapsedMilliseconds;
    }

    private void SetValueIn(int millis) {
      ThreadPool.QueueUserWorkItem(o => {
        Thread.Sleep(millis);
        mFuture.Set(10);
      });
    }

    private void SetFailedIn(int millis, Exception ex) {
      ThreadPool.QueueUserWorkItem(o => {
        Thread.Sleep(millis);
        mFuture.Failed(ex);
      });
    }

    private ResultFuture<int> mFuture;
  }
}