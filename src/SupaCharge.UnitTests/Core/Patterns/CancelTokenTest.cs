using NUnit.Framework;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Patterns {
  [TestFixture]
  public class CancelTokenTest : BaseTestCase {
    [Test]
    public void TestDefaultIsNotCancelled() {
      Assert.That(mToken.Cancelled, Is.False);
    }

    [Test]
    public void TestCancelledTrueWhenTokenHasCancelCalled() {
      mToken.Cancel();
      Assert.That(mToken.Cancelled, Is.True);
    }

    [SetUp]
    public void DoSetup() {
      mToken = new CancelToken();
    }

    private CancelToken mToken;
  }
}