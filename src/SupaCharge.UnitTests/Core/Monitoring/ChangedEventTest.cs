using NUnit.Framework;
using SupaCharge.Core.Monitoring;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Monitoring {
  [TestFixture]
  public class ChangedEventTest : BaseTestCase {
    [Test]
    public void TestChangesToAFileResultInChangeEventBeingRaised() {
      var evt = new ChangedEvent("file1.txt");
      Assert.That(evt.FileName, Is.EqualTo("file1.txt"));
    }
  }
}