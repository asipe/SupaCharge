using System;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ExceptionHandling {
  [TestFixture]
  public class AggregatedExceptionTest : BaseTestCase {
    [Test]
    public void TestToString() {
      var ex = new AggregatedException(BA(GetException("one"), GetException("two")));
      Assert.That(ex.ToString(), Is
                                   .StringContaining("one")
                                   .And
                                   .StringContaining("two"));
    }

    private static Exception GetException(string msg) {
      try {
        ThrowException(msg);
      } catch (Exception e) {
        return e;
      }
      return null;
    }

    private static void ThrowException(string msg) {
      throw new Exception(msg);
    }
  }
}