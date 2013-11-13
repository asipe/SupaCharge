using System;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ExceptionHandling {
  [TestFixture]
  public class AggregatedExceptionTest : BaseTestCase {
    [Test]
    public void TestCreateWithNoMessageSingleException() {
      var ex = new AggregatedException(BA(GetException("one")));
      Assert.That(ex.Message, Is
                                .StringStarting("-------------------------------------" + Environment.NewLine + Environment.NewLine + Environment.NewLine)
                                .And
                                .StringContaining("one")
                                .And
                                .StringEnding("-------------------------------------" + Environment.NewLine));
    }

    [Test]
    public void TestCreateWithNoMessageMultipleExceptions() {
      var ex = new AggregatedException(BA(GetException("one"), GetException("two")));
      Assert.That(ex.Message, Is
                                .StringStarting("-------------------------------------" + Environment.NewLine + Environment.NewLine + Environment.NewLine)
                                .And
                                .StringContaining("one")
                                .And
                                .StringContaining("two")
                                .And
                                .StringEnding("-------------------------------------" + Environment.NewLine));
    }

    [Test]
    public void TestCreateWithMessageSingleException() {
      var ex = new AggregatedException(BA(GetException("one")), "hello world");
      Assert.That(ex.Message, Is
                                .StringStarting("hello world" + Environment.NewLine + "-------------------------------------" + Environment.NewLine + Environment.NewLine + Environment.NewLine)
                                .And
                                .StringContaining("one")
                                .And
                                .StringEnding("-------------------------------------" + Environment.NewLine));
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