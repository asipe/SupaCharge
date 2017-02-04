using System;
using Moq;
using NUnit.Framework;
using SupaCharge.Core.DiagnosticAbstractions;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Patterns {
  [TestFixture]
  public class CircuitBreakerTest : BaseTestCase {
    [Test]
    public void TestExcecuteWhenCircuitClosedExecutesAndShouldStayClosed() {
      mStopwatch.SetupGet(w => w.IsRunning).Returns(false);
      mStopwatch.Setup(w => w.Stop());
      Assert.That(NewBreaker(BreakCheckReturningFalse, false).Execute(WorkReturningTrue), Is.EqualTo(true));
    }

    [Test]
    public void TestExcecuteWhenCircuitClosedExecutesAndShouldOpenReturnDefault() {
      mStopwatch.SetupGet(w => w.IsRunning).Returns(false);
      mStopwatch.Setup(w => w.Reset());
      mStopwatch.Setup(w => w.Start());
      Assert.That(NewBreaker(BreakCheckReturningTrue, false).Execute(WorkThrowing), Is.EqualTo(false));
    }

    [Test]
    public void TestExcecuteWhenCircuitOpenUnderTimeoutReturnsDefaultAndShouldStayOpen() {
      mStopwatch.SetupGet(w => w.IsRunning).Returns(true);
      mStopwatch.SetupGet(w => w.ElapsedMilliseconds).Returns(1000);
      Assert.That(NewBreaker(BreakCheckReturningFalse, false).Execute(WorkReturningTrue), Is.EqualTo(false));
    }

    [Test]
    public void TestExcecuteWhenCircuitOpenOverTimeoutExecutesAndShouldClosed() {
      mStopwatch.SetupGet(w => w.IsRunning).Returns(true);
      mStopwatch.SetupGet(w => w.ElapsedMilliseconds).Returns(1001);
      mStopwatch.Setup(w => w.Stop());
      Assert.That(NewBreaker(BreakCheckReturningFalse, false).Execute(WorkReturningTrue), Is.EqualTo(true));
    }

    [Test]
    public void TestExcecuteWhenCircuitOpenOverTimeoutExecutesAndShouldStayOpen() {
      mStopwatch.SetupGet(w => w.IsRunning).Returns(true);
      mStopwatch.SetupGet(w => w.ElapsedMilliseconds).Returns(1001);
      mStopwatch.Setup(w => w.Reset());
      mStopwatch.Setup(w => w.Start());
      Assert.That(NewBreaker(BreakCheckReturningTrue, false).Execute(WorkThrowing), Is.EqualTo(false));
    }

    [Test]
    public void TestExcecuteWhenThrowingNonCircuitBreakingCheckThrows() {
      mStopwatch.SetupGet(w => w.IsRunning).Returns(false);
      Assert.Throws<Exception>(() => NewBreaker(BreakCheckReturningFalse, false).Execute(WorkThrowing));
    }

    [SetUp]
    public void DoSetup() {
      mStopwatch = Mok<IStopwatch>();
    }

    private CircuitBreaker<bool> NewBreaker(Func<Exception, bool> breakCheck, bool defaultValue) {
      return new CircuitBreaker<bool>(mStopwatch.Object, breakCheck, 1000, defaultValue);
    }

    private static bool WorkReturningTrue() {
      return true;
    }

    private static bool BreakCheckReturningFalse(Exception e) {
      return false;
    }

    private static bool BreakCheckReturningTrue(Exception e) {
      return true;
    }

    private static bool WorkThrowing() {
      throw new Exception();
    }

    private Mock<IStopwatch> mStopwatch;
  }
}