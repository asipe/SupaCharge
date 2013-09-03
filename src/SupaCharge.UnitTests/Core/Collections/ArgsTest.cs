using System;
using NUnit.Framework;
using SupaCharge.Core.Collections;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Collections {
  [TestFixture]
  public class ArgsTest : BaseTestCase {
    [Test]
    public void TestUsge() {
      var args = new Args(1, "abc", new DateTime(2013, 10, 15), this);
      Assert.That(args.Get<int>(0), Is.EqualTo(1));
      Assert.That(args.Get<string>(1), Is.EqualTo("abc"));
      Assert.That(args.Get<DateTime>(2), Is.EqualTo(new DateTime(2013, 10, 15)));
      Assert.That(args.Get<ArgsTest>(3), Is.SameAs(this));
    }
  }
}