using NUnit.Framework;
using SupaCharge.Core.Net;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Net {
  [TestFixture]
  public class NoOpDotDnsTest : BaseTestCase {
    [Test]
    public void TestNoOpDnsReturnsTheValueOriginallyGiven() {
      var dns = new NoOpDotNetDns();

      Assert.That(dns.GetIPAddress("zbeast"), Is.EqualTo("zbeast"));
    }

  }
}