using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupaCharge.Core.ConfigurationWrapper;
using NUnit.Framework;

namespace SupaCharge.UnitTests.Core.ConfigWrapper {
  [TestFixture]
  class ConfigWrapperTest {
    private ConfigurationWrapper cWrapper;
    
    [SetUp]
    public void Init() {
      var cWrapper = new ConfigurationWrapper();
    }

    [Test]
    public void BasicContains() {
      Assert.That(cWrapper.Contains("name"), Is.True);
      Assert.That(cWrapper.Contains("num"), Is.True);
    }

    [Test]
    public void BasicGet() {
      Assert.That(cWrapper.Get<string>("name"), Is.EqualTo("joe"));
      Assert.That(cWrapper.Get<int>("num"), Is.EqualTo(123));
    }

    [Test]
    public void BasicGetWithDefualtVals() {
      Assert.That(cWrapper.Get<string>("nam", "harold"), Is.EqualTo("harold"));
      Assert.That(cWrapper.Get<int>("nom-nom", 321), Is.EqualTo(321));
    }

    

  }
}
