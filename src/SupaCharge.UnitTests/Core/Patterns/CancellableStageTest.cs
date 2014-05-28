using NUnit.Framework;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Patterns {
  [TestFixture]
  public class CancellableStageTest : BaseTestCase {
    private class StubStage : CancellableStage<int> {
      public StubStage(int priority) : base(priority) {}
      public override void Execute(int context, CancelToken token) {}
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