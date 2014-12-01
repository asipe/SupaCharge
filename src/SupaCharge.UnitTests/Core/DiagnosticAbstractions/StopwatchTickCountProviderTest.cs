using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.DiagnosticAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.DiagnosticAbstractions {
  [TestFixture]
  public class StopwatchTickCountProviderTest : BaseTestCase {
    [Test]
    public void TestGetTicksMoves() {
      var current = mProvider.GetTicks();
      Thread.Sleep(15);
      Assert.That(mProvider.GetTicks(), Is.GreaterThan(current));
    }

    [Test]
    public void TestGetFrequencyDoesNotChangeBetweenCalls() {
      Assert.That(mProvider.GetFrequency(), Is.EqualTo(mProvider.GetFrequency()));
      Assert.That(mProvider.GetFrequency(), Is.EqualTo(mProvider.GetFrequency()));
    }

    [Test]
    public void TestGetFrequencyMatchesFrequency() {
      Assert.That(Stopwatch.Frequency, Is.EqualTo(mProvider.GetFrequency()));
    }

    [SetUp]
    public void DoSetup() {
      mProvider = new StopwatchTickCountProvider();
    }

    private StopwatchTickCountProvider mProvider;
  }
}