using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class HaltEventTest : BaseTestCase {
    [Test]
    public void TestConstructorInitializesToFalse() {
      Assert.That(mHalt.IsSet(), Is.False);
    }

    [Test]
    public void TestSetChangesIsSetToTrue() {
      mHalt.Set();
      Assert.That(mHalt.IsSet(), Is.True);
    }

    [Test]
    public void TestResetChangesIsSetToFalse() {
      mHalt.Set();
      mHalt.Reset();
      Assert.That(mHalt.IsSet(), Is.False);
    }

    [SetUp]
    public void SetUp() {
      mHalt = new HaltEvent();
    }

    private HaltEvent mHalt;
  }
}