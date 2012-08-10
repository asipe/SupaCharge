using System;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.ExceptionHandling {
  [TestFixture]
  public class ExceptionDumpTest : BaseTestCase {
    [Test]
    public void TestSingleException() {
      var dump = new ExceptionDump(new Exception("hello world"));
      var actual = dump.ToString();
      var split = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
      Assert.That(split, Is.EqualTo(new[] {"System.Exception", "hello world"}));
      Assert.That(split.Length, Is.EqualTo(2));
    }

    [Test]
    public void TestDumpsStackTrace() {
      var ex = BuildException();
      var dump = new ExceptionDump(ex);
      var actual = dump.ToString();
      var split = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

      Assert.That(split[1], Is.EqualTo("HI"));
      Assert.That(split[2], Is.StringContaining("at SupaCharge.UnitTests.Core.ExceptionHandling"));
      Assert.That(split.Length, Is.EqualTo(3));
    }

    [Test]
    public void TestDumpsExceptionType() {
      var ex = BuildException();
      var dump = new ExceptionDump(ex);
      var actual = dump.ToString();
      var split = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

      Assert.That(split[0], Is.EqualTo("System.Exception"));
      Assert.That(split[1], Is.EqualTo("HI"));
      Assert.That(split[2], Is.StringContaining("at SupaCharge.UnitTests.Core.ExceptionHandling"));
      Assert.That(split.Length, Is.EqualTo(3));
    }

    [Test]
    public void TestInnerExceptionDisplay() {
      var ex = new Exception("hi", new Exception("bye"));

      var dump = new ExceptionDump(ex);
      var actual = dump.ToString();
      var split = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

      Assert.That(split[0], Is.EqualTo("System.Exception"));
      Assert.That(split[1], Is.EqualTo("hi"));
      Assert.That(split[2], Is.EqualTo("----- Inner Exception"));
      Assert.That(split[3], Is.EqualTo("System.Exception"));
    }

    [Test]
    public void TestLengthyChainOfExceptions() {
      var ex = new Exception("hi", new Exception("bye", new Exception("Here", new Exception("yo", new Exception("last one")))));
      var dump = new ExceptionDump(ex);
      var actual = dump.ToString();
      var split = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

      Assert.That(split[0], Is.EqualTo("System.Exception"));
      Assert.That(split[1], Is.EqualTo("hi"));
      Assert.That(split[2], Is.EqualTo("----- Inner Exception"));
      Assert.That(split[3], Is.EqualTo("System.Exception"));
      Assert.That(split[4], Is.EqualTo("bye"));
      Assert.That(split[5], Is.EqualTo("----- Inner Exception"));
      Assert.That(split[6], Is.EqualTo("System.Exception"));
      Assert.That(split[7], Is.EqualTo("Here"));
      Assert.That(split[8], Is.EqualTo("----- Inner Exception"));
      Assert.That(split[9], Is.EqualTo("System.Exception"));
      Assert.That(split[10], Is.EqualTo("yo"));
      Assert.That(split[11], Is.EqualTo("----- Inner Exception"));
      Assert.That(split[12], Is.EqualTo("System.Exception"));
      Assert.That(split[13], Is.EqualTo("last one"));
    }

    private static Exception BuildException() {
      try {
        throw new Exception("HI");
      } catch (Exception ex) {
        return ex;
      }
    }
  }
}