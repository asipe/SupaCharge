using NUnit.Framework;
using SupaCharge.Core.OID;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.OID {
  [TestFixture]
  public class GuidOIDProviderTest : BaseTestCase {
    [Test]
    public void TestGetID() {
      var oid = new GuidOIDProvider().GetID();
      Assert.That(oid.Length, Is.EqualTo(32));
    }
  }
}