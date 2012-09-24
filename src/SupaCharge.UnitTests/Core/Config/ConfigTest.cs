using NUnit.Framework;
using SupaCharge.Core.Config;

namespace SupaCharge.UnitTests.Core.ConfigWrapper {
  [TestFixture]
  internal class ConfigWrapperTest {
    [Test]
    public void TestContainsTrue() {
      Assert.That(mWrapper.Contains("name"), Is.True);
      Assert.That(mWrapper.Contains("num"), Is.True);
    }

    [Test]
    public void TestContainsFalse() {
      Assert.That(mWrapper.Contains("nom-nom"), Is.False);
    }

    [Test]
    public void TestGetString() {
      Assert.That(mWrapper.Get<string>("name"), Is.EqualTo("joe"));
    }

    [Test]
    public void TestGetInt() {
      Assert.That(mWrapper.Get<int>("num"), Is.EqualTo(123));
    }

    [Test]
    public void TestGetWithDefaultValueReturnsDefaultIfKeyNotFoundString() {
      Assert.That(mWrapper.Get("nam", "harold"), Is.EqualTo("harold"));
    }

    [Test]
    public void TestGetWithDefaultValueReturnsDefaultIfKeyNotFoundInt() {
      Assert.That(mWrapper.Get("nom-nom", 321), Is.EqualTo(321));
    }

    [Test]
    public void TestGetWithDefaultValueReturnsValueIfKeyFound() {
      Assert.That(mWrapper.Get("name", "bo-bop"), Is.EqualTo("joe"));
    }

    private readonly AppConfig mWrapper = new AppConfig();
  }
}