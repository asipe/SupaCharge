using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupaCharge.Core.ConfigurationWrapper;
using NUnit.Framework;

namespace SupaCharge.UnitTests.Core.ConfigWrapper {
  [TestFixture]
  class ConfigWrapperTest {
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
      Assert.That(mWrapper.Get<string>("nam", "harold"), Is.EqualTo("harold"));
    }

    [Test] 
    public void TestGetWithDefaultValueReturnsDefaultIfKeyNotFoundInt() {
      Assert.That(mWrapper.Get<int>("nom-nom", 321), Is.EqualTo(321));
    }
    
    [Test]
    public void TestGetWithDefaultValueReturnsValueIfKeyFound() {
      Assert.That(mWrapper.Get<string>("name", "bo-bop"), Is.EqualTo("joe"));
    }
    
    private ConfigurationWrapper mWrapper = new ConfigurationWrapper();
  }
}

