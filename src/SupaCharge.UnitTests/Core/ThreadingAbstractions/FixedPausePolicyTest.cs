using Moq;
using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class FixedPausePolicyTest : BaseTestCase {
    [Test]
    public void TestPauseDelegatesToSleeper() {
      mSleeper.Setup(s => s.Sleep(50));
      mPolicy.Pause();
      mSleeper.Verify(s => s.Sleep(50), Times.Once());
      mPolicy.Pause();
      mSleeper.Verify(s => s.Sleep(50), Times.Exactly(2));
      mPolicy.Pause();
      mSleeper.Verify(s => s.Sleep(50), Times.Exactly(3));
    }

    [SetUp]
    public void DoSetup() {
      mSleeper = Mok<ISleep>();
      mPolicy = new FixedPausePolicy(mSleeper.Object, 50);
    }

    private Mock<ISleep> mSleeper;
    private FixedPausePolicy mPolicy;
  }
}