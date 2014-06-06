using NUnit.Framework;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Patterns {
  [TestFixture]
  public class CancellableStageTest : BaseTestCase {
    private class StubStage : CancellableStage<int> {
      public StubStage(int priority) : base(priority) {}
      public CancelToken LastToken{get;private set;}
      public int LastContext{get;private set;}

      public override void Execute(int context, CancelToken token) {
        LastContext = context;
        LastToken = token;
      }
    }

    [Test]
    public void TestPriority() {
      Assert.That(mStage.Priority, Is.EqualTo(33));
    }

    [Test]
    public void TestExecuteWithNoTokenCallsExecuteCorrectly() {
      mStage.Execute(44);
      Assert.That(mStage.LastContext, Is.EqualTo(44));
      Assert.That(mStage.LastToken, Is.Null);
    }

    [SetUp]
    public void DoSetup() {
      mStage = new StubStage(33);
    }

    private StubStage mStage;
  }
}