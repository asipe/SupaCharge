using Moq;
using NUnit.Framework;
using SupaCharge.Core.DiagnosticAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.DiagnosticAbstractions {
  [TestFixture]
  public class RecordingEventListTest : BaseTestCase {
    [Test]
    public void TestDefaultEventListHasInitialEntry() {
      Assert.That(mList.GetEvents(), Is.EqualTo(BA("Frequency: 1", "----------", "0: Created")));
    }

    [Test]
    public void TestAddingEvents() {
      mProvider.Setup(p => p.GetTicks()).Returns(1500);
      mList.AddEvent("event1");
      Assert.That(mList.GetEvents(), Is.EqualTo(BA("Frequency: 1", "----------", "0: Created", "500: event1")));
      mProvider.Setup(p => p.GetTicks()).Returns(1750);
      mList.AddEvent("event2 {0} {1}", "a", 1);
      Assert.That(mList.GetEvents(), Is.EqualTo(BA("Frequency: 1", "----------", "0: Created", "500: event1", "750: event2 a 1")));
    }

    [SetUp]
    public void DoSetup() {
      mProvider = Mok<ITickCountProvider>();
      mProvider.Setup(p => p.GetFrequency()).Returns(1);
      mProvider.Setup(p => p.GetTicks()).Returns(1000);
      mList = new RecordingEventList(mProvider.Object);
    }

    private RecordingEventList mList;
    private Mock<ITickCountProvider> mProvider;
  }
}