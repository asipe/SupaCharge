using Moq;
using NUnit.Framework;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ThreadingAbstractions {
  [TestFixture]
  public class IncreasingPausePolicyTest : BaseTestCase {
    [Test]
    public void TestPauseDelegatesToSleeper() {
      mSleeper.Setup(s => s.Sleep(10));
      mPolicy.Pause();
      mSleeper.Setup(s => s.Sleep(15));
      mPolicy.Pause();
      mSleeper.Setup(s => s.Sleep(22));
      mPolicy.Pause();
      mSleeper.Setup(s => s.Sleep(33));
      mPolicy.Pause();
      mSleeper.Setup(s => s.Sleep(49));
      mPolicy.Pause();
    }

    [SetUp]
    public void DoSetup() {
      mSleeper = Mok<ISleep>();
      mPolicy = new IncreasingPausePolicy(mSleeper.Object, 10, 50);
    }

    private Mock<ISleep> mSleeper;
    private IncreasingPausePolicy mPolicy;
  }
}