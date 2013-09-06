using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class RetryTest : BaseTestCase {
    private class WorkInfo {
      public int Iteration{get;set;}
      public long Millis{get;set;}
    }

    [Test]
    public void TestStartCallsWorkOnceAndExitsIfSuccess() {
      new Retry(10, 100)
        .WithWork(DoWork)
        .Start();

      Assert.That(mList.Count, Is.EqualTo(1));
      Assert.That(mList[0].Iteration, Is.EqualTo(0));
      Assert.That(mList[0].Millis, Is.LessThan(25));
    }

    [Test]
    public void TestStartCallsWorkUntilThereIsSuccess() {
      mThrowUntil = 5;

      new Retry(10, 50)
        .WithWork(DoWork)
        .Start();

      Assert.That(mList.Count, Is.EqualTo(6));
      Assert.That(mList.Select(i => i.Iteration), Is.EqualTo(BA(0, 1, 2, 3, 4, 5)));
      Assert.That(mList[0].Millis, Is.LessThan(25));
      Assert.That(mList.Skip(1).Select(i => i.Millis), Is.All.GreaterThan(40).And.All.LessThan(100));
    }

    [Test]
    public void TestStartThrowsWhenThereIsNoSuccessAndAllIterationsAreExpended() {
      mThrowUntil = int.MaxValue;

      var ex = Assert.Throws<Exception>(() => new Retry(5, 50).WithWork(DoWork).Start());
      Assert.That(ex.Message, Is.EqualTo("throwing on iteration: 4"));

      Assert.That(mList.Count, Is.EqualTo(5));
      Assert.That(mList.Select(i => i.Iteration), Is.EqualTo(BA(0, 1, 2, 3, 4)));
      Assert.That(mList[0].Millis, Is.LessThan(25));
      Assert.That(mList.Skip(1).Select(i => i.Millis), Is.All.GreaterThan(40).And.All.LessThan(100));
    }

    [Test]
    public void TestStartCallsErrorHandlerWhenThereIsNoSuccessAndErrorHandlerIsDefined() {
      mThrowUntil = int.MaxValue;
      Exception lastException = null;

      new Retry(5, 50)
        .WithWork(DoWork)
        .WithErrorHandler(ex => lastException = ex)
        .Start();

      Assert.That(lastException.Message, Is.EqualTo("throwing on iteration: 4"));
      Assert.That(mList.Count, Is.EqualTo(5));
      Assert.That(mList.Select(i => i.Iteration), Is.EqualTo(BA(0, 1, 2, 3, 4)));
      Assert.That(mList[0].Millis, Is.LessThan(25));
      Assert.That(mList.Skip(1).Select(i => i.Millis), Is.All.GreaterThan(40).And.All.LessThan(100));
    }

    [SetUp]
    public void DoSetup() {
      mWatch = Stopwatch.StartNew();
      mList = new List<WorkInfo>();
      mThrowUntil = -1;
    }

    private Stopwatch mWatch;
    private List<WorkInfo> mList;
    private int mThrowUntil;

    private void DoWork(int x) {
      mList.Add(new WorkInfo {Iteration = x, Millis = mWatch.ElapsedMilliseconds});
      mWatch.Reset();
      mWatch.Start();

      if (mThrowUntil == -1)
        return;

      if (x != mThrowUntil)
        throw new Exception("throwing on iteration: " + x);
    }
  }
}