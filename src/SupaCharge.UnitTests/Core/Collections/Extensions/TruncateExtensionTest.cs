using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SupaCharge.Core.Collections.Extensions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections.Extensions {
    [TestFixture]
  public class TruncateExtensionTest: BaseTestCase {
      [Test]
      public void TestExtensionMethod() {
        var ary = new[] {1,2,3,4};
        var zeroAry = new string[0];
        var bigArry = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
       
        Assert.That(ary.Truncate(1), Is.EqualTo(new[] {1,2,3}));
        Assert.That(zeroAry.Truncate(1), Is.Empty);
        Assert.That(ary.Truncate(2), Is.EqualTo(new[] {1,2}));
        Assert.That(bigArry.Truncate(10), Is.EqualTo(new[] { 1, 2, 3 }));
        Assert.That(bigArry.Truncate(123), Is.Empty);

      }
  }
}
