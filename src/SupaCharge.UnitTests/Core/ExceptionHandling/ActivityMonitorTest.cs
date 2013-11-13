using System;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ExceptionHandling {
  [TestFixture]
  public class ActivityMonitorTest : BaseTestCase {
    [Test]
    public void TestResolveDoesNotThrowByDefault() {
      mList.Resolve();
    }

    [Test]
    public void TestResolveDoesNotThrowIfSingleActivitySucceeded() {
      var count = 0;
      mList.Monitor(() => ++count);
      mList.Resolve();
      Assert.That(count, Is.EqualTo(1));
    }

    [Test]
    public void TestResolveDoesNotThrowIfMultipleActivitiesSucceed() {
      var count = 0;
      mList.Monitor(() => ++count);
      mList.Monitor(() => ++count);
      mList.Monitor(() => ++count);
      mList.Resolve();
      Assert.That(count, Is.EqualTo(3));
    }

    [Test]
    public void TestResolveThrowsIfSingleActivityFailed() {
      mList.Monitor(() => {throw new Exception("test error");});
      var ex = Assert.Throws<AggregatedException>(() => mList.Resolve());
      Assert.That(ex.Message, Is
                                .StringStarting("1 Activities Failed")
                                .And
                                .StringContaining("test error"));
    }

    [Test]
    public void TestResolveThrowsIfMultipleActivitiesFailed() {
      mList.Monitor(() => {throw new Exception("test error 1");});
      mList.Monitor(() => {throw new Exception("test error 2");});
      mList.Monitor(() => {throw new Exception("test error 3");});
      var ex = Assert.Throws<AggregatedException>(() => mList.Resolve());
      Assert.That(ex.Message, Is
                                .StringStarting("3 Activities Failed")
                                .And
                                .StringContaining("test error 1")
                                .And
                                .StringContaining("test error 2")
                                .And
                                .StringContaining("test error 3"));
    }

    [SetUp]
    public void DoSetup() {
      mList = new ActivityMonitor();
    }

    private ActivityMonitor mList;
  }
}