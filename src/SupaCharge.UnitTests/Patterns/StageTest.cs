using NUnit.Framework;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Patterns {
  [TestFixture]
  public class StageTest : BaseTestCase {
    private class StubStage : Stage<int> {
      public StubStage(int priority) : base(priority) {}
    }

    [Test]
    public void TestPriority() {
      Assert.That(mStage.Priority, Is.EqualTo(33));
    }

    [SetUp]
    public void DoSetup() {
      mStage = new StubStage(33);
    }

    private StubStage mStage;
  }
}