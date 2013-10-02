using System;
using NUnit.Framework;
using SupaCharge.Core.Events;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Events {
  [TestFixture]
  public class RaiseEventTest : BaseTestCase {
    public class TestEvent : EventArgs {
      public TestEvent(string testPhrase) {
        TestPhrase = testPhrase;
      }

      public string TestPhrase{get;set;}
    }

    [Test]
    public void TestChangesToAFileResultInChangeEventBeingRaised() {
      mTestEvent = new TestEvent("testing");
      TestEventHandler += (o, a) => mTestEvent.TestPhrase = "gnitset";
      TestEventHandler.RaiseEvent(null, mTestEvent);
      Assert.That(mTestEvent.TestPhrase, Is.EqualTo("gnitset"));
    }

    public event EventHandler<TestEvent> TestEventHandler;
    private TestEvent mTestEvent;
  }
}