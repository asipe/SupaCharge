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
    public void BasicContains() {
      Assert.That(mWrapper.Contains("name"), Is.True);
      Assert.That(mWrapper.Contains("num"), Is.True);
      Assert.That(mWrapper.Contains("nom-nom"), Is.False);
    }

    [Test]
    public void BasicGet() {
      Assert.That(mWrapper.Get<string>("name"), Is.EqualTo("joe"));
      Assert.That(mWrapper.Get<int>("num"), Is.EqualTo(123));
    }

    [Test]
    public void BasicGetWithDefualtVals() {
      Assert.That(mWrapper.Get<string>("nam", "harold"), Is.EqualTo("harold"));
      Assert.That(mWrapper.Get<int>("nom-nom", 321), Is.EqualTo(321));
      Assert.That(mWrapper.Get<string>("name", "bo-bop"), Is.EqualTo("joe"));
    }
    
    private ConfigurationWrapper mWrapper = new ConfigurationWrapper();
  }
}
