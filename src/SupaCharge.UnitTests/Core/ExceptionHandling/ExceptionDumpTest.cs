using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SupaCharge.Testing;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;

namespace SupaCharge.UnitTests.Core.ExceptionHandling {
  [TestFixture]
  public class ExceptionDumpTest : BaseTestCase {
    [Test]
    public void TestDumpsMessage() {
      var dump = new ExceptionDump(new Exception("hello world"));
      Assert.That(dump.ToString(), Is.EqualTo("hello world"));
    }

  }
}
