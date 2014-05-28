using NUnit.Framework;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Patterns {
  [TestFixture]
  public class StageTest : BaseTestCase {
    private class StubStage : Stage<int> {
      public StubStage(int priority) : base(priority) {}
      public int LastContext{get;private set;}

      protected override void DoExecute(int context) {
        LastContext = context;
      }
    }

    [Test]
    public void TestPriority() {
      Assert.That(mStage.Priority, Is.EqualTo(33));
    }

    [Test]
    public void TestExecuteDelegatesContextToDoExecute() {
      mStage.Execute(15, null);
      Assert.That(mStage.LastContext, Is.EqualTo(15));
    }

    [SetUp]
    public void DoSetup() {
      mStage = new StubStage(33);
    }

    private StubStage mStage;
  }
}