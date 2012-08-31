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
      var CWrapper = new ConfigurationWrapper();

      Assert.That(CWrapper.Contains("name"), Is.True);
      Assert.That(CWrapper.Contains("num"), Is.True);
    }

    [Test]
    public void BasicGet() {
      var CWrapper = new ConfigurationWrapper();

      Assert.That(CWrapper.Get<string>("name"), Is.EqualTo("joe"));
      Assert.That(CWrapper.Get<int>("num"), Is.EqualTo(123));
    }

    [Test]
    public void BasicGetWithDefualtVals() {
      var CWrapper = new ConfigurationWrapper();

      Assert.That(CWrapper.Get<string>("nam", "harold"), Is.EqualTo("harold"));
      Assert.That(CWrapper.Get<int>("nom-nom", 321), Is.EqualTo(321));
    }
  }
}
