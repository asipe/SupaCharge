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
      var actual = dump.ToString();
      var split = actual.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      Assert.That(split[1].ToString(), Is.EqualTo("hello world"));
    }

    [Test]
    public void TestDumpsStackTrace() {
      var ex = BuildException();
      var dump = new ExceptionDump(ex);
      var actual = dump.ToString();
      var split = actual.Split(new [] {Environment.NewLine}, StringSplitOptions.None);
      
      Assert.That(split[1], Is.EqualTo("HI"));
      Assert.That(split[2], Is.StringContaining("at SupaCharge.UnitTests.Core.ExceptionHandling"));
      Assert.That(split.Length, Is.EqualTo(3));
    }

    [Test]
    public void TestDumpsExceptionType() {
      var ex = BuildException();
      var dump = new ExceptionDump(ex);
      var actual = dump.ToString();
      var split = actual.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      
      Assert.That(split[0], Is.EqualTo("System.Exception"));
      Assert.That(split[1], Is.EqualTo("HI"));
      Assert.That(split[2], Is.StringContaining("at SupaCharge.UnitTests.Core.ExceptionHandling"));
      Assert.That(split.Length, Is.EqualTo(3));
    }

  //  [Test]
  //  public void TestInnerExceptionDisplay() {
   //   var ex = BuildException();
   //   var dump = new ExceptionDump(ex);
//var actual = dump.ToString();
   //   var split = actual.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

    //  Assert.That(split[0], Is.EqualTo("System.Exception"));
    //  Assert.That(split[1], Is.EqualTo("HI"));
    //  Assert.That(split[2], Is.StringContaining("at SupaCharge.UnitTests.Core.ExceptionHandling"));
    //}

    private Exception BuildException() {
      try {
        throw new Exception("HI");
      }
      catch (Exception ex) {
        return ex;
      }
    }

  }
}


//exception type
//message
//stack trace
//--- inner exception
//    exception type
//    message
//    stack trace
//    ---- inner exception
//    repeat