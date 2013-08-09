using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.DiagnosticAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.DiagnosticAbstractions {
  [TestFixture]
  public class StopwatchTickCountProviderTest : BaseTestCase {
    [Test]
    public void TestGetTicksMoves() {
      var provider = new StopwatchTickCountProvider();
      var current = provider.GetTicks();
      Thread.Sleep(15);
      Assert.That(provider.GetTicks(), Is.GreaterThan(current));
    }
  }
}