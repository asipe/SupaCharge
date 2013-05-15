using System.Xml.Linq;
using System.Xml.XPath;
using Moq;
using NUnit.Framework;
using SupaCharge.Core.Config;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Config {
  [TestFixture]
  internal class ConfigWriterTests : BaseTestCase {
    [Test]
    public void TestSaveUnchangedConfigSavesSameXML() {
      mWriter = CreateWriter(_SingleEntryXml);
      mWriter.Save();
      VerifyKeyValue("user1", "joe");
    }

    [Test]
    public void TestSavingAChangedConfigChangesTheValuesOnASingleEntryConfig() {
      mWriter = CreateWriter(_SingleEntryXml);
      mWriter.Set("user1", "bob");
      mWriter.Save();
      VerifyKeyValue("user1", "bob");
    }

    [Test]
    public void TestSavingAnUnchangedConfigGivesTheSameXMLWithMultipleEntries() {
      mWriter = CreateWriter(_MultiEntryXml);
      mWriter.Save();
      VerifyKeyValue("user1", "joe");
      VerifyKeyValue("user2", "bob");
      VerifyKeyValue("user3", "hal");
    }

    [Test]
    public void TestSavingASingleConfigHasCorrectlyAlteredEntryAmongMultipleEntries() {
      mWriter = CreateWriter(_MultiEntryXml);
      mWriter.Set("user1", "sam");
      mWriter.Save();
      VerifyKeyValue("user1", "sam");
    }

    [Test]
    public void TestSavingMultipleChangedConfigsHasCorrectlyChangedValues() {
      mWriter = CreateWriter(_MultiEntryXml);
      mWriter.Set("user1", "sam");
      mWriter.Set("user2", "tim");
      mWriter.Set("user3", "ted");
      mWriter.Save();
      VerifyKeyValue("user1", "sam");
      VerifyKeyValue("user2", "tim");
      VerifyKeyValue("user3", "ted");
    }

    [Test]
    public void TestFullFunctionalityGivenMultipleSavesAndSetsInMixedOrder() {
      mWriter = CreateWriter(_MultiEntryXml);
      mWriter.Set("user1", "sam");
      mWriter.Set("user2", "tim");
      mWriter.Save();
      mWriter.Set("user3", "ted");
      mWriter.Save();
      mWriter.Save();
      VerifyKeyValue("user1", "sam");
      VerifyKeyValue("user2", "tim");
      VerifyKeyValue("user3", "ted");
    }

    [SetUp]
    public void DoSetup() {
      mFile = Mok<IFile>();
      mFile.Setup(f => f.WriteAllText("config.xml", It.IsAny<string>()));
    }

    private void VerifyKeyValue(string key, string value) {
      mFile.Verify(f => f.WriteAllText("config.xml", It.Is<string>(s => XDocument
                                                                          .Parse(s)
                                                                          .XPathSelectElement(string.Format("//configuration/appSettings/add[@key='{0}']", key))
                                                                          .Attribute("value")
                                                                          .Value == value)));
    }

    private ConfigWriter CreateWriter(string config) {
      mFile.Setup(f => f.ReadAllText("config.xml")).Returns(config);
      return new ConfigWriter(mFile.Object, "config.xml");
    }

    private Mock<IFile> mFile;
    private ConfigWriter mWriter;

    private const string _SingleEntryXml =
      @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <appSettings>
    <add key=""user1"" value=""joe"" />
  </appSettings>
</configuration>";

    private const string _MultiEntryXml =
      @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <appSettings>
    <add key=""user1"" value=""joe"" />
    <add key=""user2"" value=""bob"" />
    <add key=""user3"" value=""hal"" />
  </appSettings>
</configuration>";
  }
}