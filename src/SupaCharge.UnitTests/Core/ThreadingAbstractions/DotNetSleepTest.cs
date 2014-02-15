using System.Diagnostics;
using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class DotNetSleepTest : BaseTestCase {
    [Test]
    public void TestSleepSleeps() {
      var sleeper = new DotNetSleep();
      var sw = Stopwatch.StartNew();
      sleeper.Sleep(50);
      Assert.That(sw.ElapsedMilliseconds, Is.GreaterThan(30).And.LessThan(75));
    }
  }
}