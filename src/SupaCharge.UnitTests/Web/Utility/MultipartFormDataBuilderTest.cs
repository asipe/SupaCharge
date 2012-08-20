using NUnit.Framework;
using SupaCharge.Core.Web.Utility;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Web.Utility {
  [TestFixture]
  public class MultipartFormDataBuilderTest : BaseTestCase {
    [Test]
    public void TestEmptyGivesEmpty() {
      Assert.That(mBuilder.Build(), Is.Empty);
    }

    [Test]
    public void TestSingleValue() {
      mBuilder.Add("a", "b");
      const string expected = "\r\n\r\n----abc\r\ncontent-disposition: form-data; name=\"a\"\r\n\r\nb\r\n----abc--";
      Assert.That(mBuilder.Build(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMultipleValues() {
      mBuilder.Add("val1", "value1");
      mBuilder.Add("val2", 33);
      mBuilder.Add("another", "a larger value");
      const string expected = "\r\n\r\n----abc\r\ncontent-disposition: form-data; name=\"val1\"\r\n\r\nvalue1\r\n----abc\r\ncontent-disposition: form-data; name=\"val2\"\r\n\r\n33\r\n----abc\r\ncontent-disposition: form-data; name=\"another\"\r\n\r\na larger value\r\n----abc--";
      Assert.That(mBuilder.Build(), Is.EqualTo(expected));
    }

    [SetUp]
    public void DoSetup() {
      mBuilder = new MultipartFormDataBuilder("--abc");
    }

    private MultipartFormDataBuilder mBuilder;
  }
}