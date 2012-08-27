using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupaCharge.Testing;
using NUnit.Framework;
using SupaCharge.Core.Converter;

namespace SupaCharge.UnitTests.Core.Converter {
  [TestFixture]
  public class ValueConverterTest:BaseTestCase {
    [Test]
    public void TestBasicConversion() {
      var converter = new ValueConverter();
      Assert.That(converter.Get<int>("3"), Is.EqualTo(3));
      Assert.That(converter.Get<int>("yo"), Is.EqualTo(3));
    }
    
  }
}
