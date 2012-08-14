using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SupaCharge.Core.Web.Utility;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Web.Utility {
  [TestFixture]
  public class FormUrlEncodedDataParserTest : BaseTestCase {
    [TestCaseSource("GetParsingTests")]
    public void TestParsing(string data, Dictionary<string, string> expected) {
      var actual = mParser.Parse(data);
      Assert.That(actual.Count, Is.EqualTo(expected.Count));
      foreach (var key in expected.Keys)
        Assert.That(actual[key], Is.EqualTo(expected[key]));
    }

    public IEnumerable GetParsingTests() {
      yield return new TestCaseData("key1=value1", new Dictionary<string, string> {{"key1", "value1"}}).SetName("SingleValue");
      yield return new TestCaseData("key1=value1&key2=value2", new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}}).SetName("MultipleValues");
      yield return new TestCaseData("key1=value 1&key2= value2 ", new Dictionary<string, string> {{"key1", "value 1"}, {"key2", " value2 "}}).SetName("MultipleValuesWithWhiteSpace");
      yield return new TestCaseData("key1=&key2=", new Dictionary<string, string> {{"key1", ""}, {"key2", ""}}).SetName("MissingValues");

      var data = string.Format("key1={0}&key2=value2", Uri.EscapeDataString("<h\\el/lo= & goodbye=>"));
      yield return new TestCaseData(data, new Dictionary<string, string> {{"key1", "<h\\el/lo= & goodbye=>"}, {"key2", "value2"}}).SetName("EncodedValue");
      yield return new TestCaseData("", new Dictionary<string, string>()).SetName("EmptyValue");
      yield return new TestCaseData("       ", new Dictionary<string, string>()).SetName("SpacesOnlyValue");
      yield return new TestCaseData(null, new Dictionary<string, string>()).SetName("NullValue");
    }

    private readonly FormUrlEncodedDataParser mParser = new FormUrlEncodedDataParser();
  }
}