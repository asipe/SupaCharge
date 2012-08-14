using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SupaCharge.Core.Web.Utility;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Web.Utility {
  [TestFixture]
  public class MultipartFormDataParserTest : BaseTestCase {
    [TestCaseSource("GetParseTests")]
    public void TestParse(string data, Dictionary<string, object> expected) {
      var actual = mParser.Parse("multipart/form-data; boundary=---xyz", data);
      Assert.That(actual.Count, Is.EqualTo(expected.Count));
      foreach (var key in expected.Keys)
        Assert.That(actual[key], Is.EqualTo(expected[key]));
    }

    [SetUp]
    public void DoSetup() {
      mParser = new MultipartFormDataParser();
    }

    public IEnumerable GetParseTests() {
      yield return new TestCaseData("", new Dictionary<string, object>()).SetName("TestEmptyDataGivesEmptyResult");
      yield return new TestCaseData("  ", new Dictionary<string, object>()).SetName("TestSpacesOnlyDataGivesEmptyResult");
      yield return new TestCaseData(null, new Dictionary<string, object>()).SetName("TestNullDataGivesEmptyResult");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\n\r\nvalue1\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", "value1"}}).SetName("TestSingleValue");
      yield return new TestCaseData("\r\n\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\n\r\nvalue1\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", "value1"}}).SetName("TestSingleValueLeadingMultipleCRLF");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\n\r\nvalue1\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field2\"\r\n\r\nvalue2\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", "value1"}, {"field2", "value2"}}).SetName("TestMultipleValues");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\n\r\n\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", ""}}).SetName("TestSingleEmptyValue");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\n\r\n\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field2\"\r\n\r\n\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", ""}, {"field2", ""}}).SetName("TestMultipleEmptyValues");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"\"\r\n\r\nvalue1\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object>()).SetName("TestEmptyKeyGivesNoValues");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\n\r\nvalue1\r\npart2\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", "value1\r\npart2"}}).SetName("TestSingleMultiLineValue");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\ncontent-type: application/zip\r\n\r\nvalue1\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", "value1"}}).SetName("TestSingleValueWithContentType");
      yield return new TestCaseData("\r\n-----xyz\r\ncontent-disposition: form-data; name=\"field1\"\r\ncontent-type: application/zip\r\ncontent-transfer-encoding: binary\r\n\r\nvalue1\r\n-----xyz--\r\n\r\n",
                                    new Dictionary<string, object> {{"field1", "value1"}}).SetName("TestSingleValueWithContentEncoding");
    }

    private MultipartFormDataParser mParser;
  }
}