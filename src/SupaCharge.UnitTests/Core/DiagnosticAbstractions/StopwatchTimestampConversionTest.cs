using System.Diagnostics;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.DiagnosticAbstractions {
  [TestFixture]
  public class StopwatchTimestampConversionTest : BaseTestCase {
    [Test]
    public void TestConversion() {
      var ticksPerSecond = Stopwatch.Frequency;
      var ticksPerMillisecond = ticksPerSecond / 1000;
      var sample1 = Stopwatch.GetTimestamp();
      Thread.Sleep(250);
      var sample2 = Stopwatch.GetTimestamp();
      Assert.That((sample2 - sample1) / ticksPerMillisecond, Is
                                                               .GreaterThan(245)
                                                               .And
                                                               .LessThan(255));
      Thread.Sleep(125);
      var sample3 = Stopwatch.GetTimestamp();
      Assert.That((sample3 - sample1) / ticksPerMillisecond, Is
                                                               .GreaterThan(370)
                                                               .And
                                                               .LessThan(395));
      Assert.That((sample3 - sample2) / ticksPerMillisecond, Is
                                                               .GreaterThan(120)
                                                               .And
                                                               .LessThan(150));
    }
  }
}