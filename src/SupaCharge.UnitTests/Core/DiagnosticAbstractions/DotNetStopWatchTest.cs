using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.DiagnosticAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.DiagnosticAbstractions {
  [TestFixture]
  public class DotNetStopWatchTest : BaseTestCase {
    [Test]
    public void TestDefaultCtorIsNotRunning() {
      Assert.That(new DotNetStopwatch().IsRunning, Is.False);
    }

    [Test]
    public void TestStopDelegates() {
      mWatch.Start();
      mDotNetwatch.Stop();
      Assert.That(mWatch.IsRunning, Is.False);
    }

    [Test]
    public void TestStartDelegates() {
      mDotNetwatch.Start();
      Assert.That(mWatch.IsRunning, Is.True);
    }

    [Test]
    public void TestIsRunningDelegates() {
      Assert.That(mDotNetwatch.IsRunning, Is.False);
      mWatch.Start();
      Assert.That(mDotNetwatch.IsRunning, Is.True);
    }

    [Test]
    public void TestResetDelegates() {
      mWatch.Start();
      Thread.Sleep(25);
      mDotNetwatch.Reset();
      Assert.That(mWatch.IsRunning, Is.False);
      Assert.That(mWatch.ElapsedMilliseconds, Is.EqualTo(0));
    }

    [Test]
    public void TestElapsedPropertiesDelegate() {
      mWatch.Start();
      Thread.Sleep(25);
      mWatch.Stop();
      Assert.That(mDotNetwatch.Elapsed, Is.EqualTo(mWatch.Elapsed));
      Assert.That(mDotNetwatch.ElapsedMilliseconds, Is.EqualTo(mWatch.ElapsedMilliseconds));
      Assert.That(mDotNetwatch.ElapsedTicks, Is.EqualTo(mWatch.ElapsedTicks));
    }

    [Test]
    public void TestStartNewIsRunning() {
      Assert.That(DotNetStopwatch.StartNew().IsRunning, Is.True);
    }

    [SetUp]
    public void DoSetup() {
      mWatch = new Stopwatch();
      mDotNetwatch = new DotNetStopwatch(mWatch);
      Assert.That(mWatch.IsRunning, Is.False);
    }

    private Stopwatch mWatch;
    private DotNetStopwatch mDotNetwatch;
  }
}