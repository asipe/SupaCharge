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
      mFile.Setup(f => f.WriteAllText("config.xml", It.IsAny<string>()));
      mWriter.Save();
      mFile.Verify(f => f.WriteAllText("config.xml", It.Is<string>(s => XDocument
                                                                          .Parse(s)
                                                                          .XPathSelectElement("//configuration/appSettings/add")
                                                                          .Attribute("value")
                                                                          .Value == "joe")));
    }

    [Test]
    public void TestSavingAChangedConfigChangesTheValuesOnASingleEntryConfig() {
      mFile.Setup(f => f.WriteAllText("config.xml", It.IsAny<string>()));
      mWriter.Set("name", "bob");
      mWriter.Save();
      mFile.Verify(f => f.WriteAllText("config.xml", It.Is<string>(s => XDocument
                                                                          .Parse(s)
                                                                          .XPathSelectElement("//configuration/appSettings/add")
                                                                          .Attribute("value")
                                                                          .Value == "bob")));
    }

    [Test]
    public void TestSavingAnUnchangedConfigGivesTheSameXMLWithMultipleEntries() {
      mFile.Setup(f => f.WriteAllText("bigConfig.xml", It.IsAny<string>()));
      mBigWriter.Save();
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user1']")
                                                                             .Attribute("value")
                                                                             .Value == "joe")));
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user2']")
                                                                             .Attribute("value")
                                                                             .Value == "bob")));
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user3']")
                                                                             .Attribute("value")
                                                                             .Value == "hal")));
    }

    [Test]
    public void TestSavingASingleConfigHasCorrectlyAlteredEntryAmongMultipleEntries() {
      mFile.Setup(f => f.WriteAllText("bigConfig.xml", It.IsAny<string>()));
      mBigWriter.Set("user1", "sam");
      mBigWriter.Save();
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user1']")
                                                                             .Attribute("value")
                                                                             .Value == "sam")));
    }

    [Test]
    public void TestSavingMultipleChangedConfigsHasCorrectlyChangedValues() {
      mFile.Setup(f => f.WriteAllText("bigConfig.xml", It.IsAny<string>()));
      mBigWriter.Set("user1", "sam");
      mBigWriter.Set("user2", "tim");
      mBigWriter.Set("user3", "ted");
      mBigWriter.Save();
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user1']")
                                                                             .Attribute("value")
                                                                             .Value == "sam")));
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user2']")
                                                                             .Attribute("value")
                                                                             .Value == "tim")));
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user3']")
                                                                             .Attribute("value")
                                                                             .Value == "ted")));
    }

    [Test]
    public void TestFullFunctionalityGivenMultipleSavesAndSetsInMixedOrder() {
      mFile.Setup(f => f.WriteAllText("bigConfig.xml", It.IsAny<string>()));
      mBigWriter.Set("user1", "sam");
      mBigWriter.Set("user2", "tim");
      mBigWriter.Save();
      mBigWriter.Set("user3", "ted");
      mBigWriter.Save();
      mBigWriter.Save();
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user1']")
                                                                             .Attribute("value")
                                                                             .Value == "sam")));
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user2']")
                                                                             .Attribute("value")
                                                                             .Value == "tim")));
      mFile.Verify(f => f.WriteAllText("bigConfig.xml", It.Is<string>(s => XDocument
                                                                             .Parse(s)
                                                                             .XPathSelectElement("//configuration/appSettings/add[@key='user3']")
                                                                             .Attribute("value")
                                                                             .Value == "ted")));
    }

    [SetUp]
    public void DoSetup() {
      mFile = Mok<IFile>();
      mFile.Setup(f => f.ReadAllText("config.xml")).Returns(_SingleEntryXml);
      mFile.Setup(f => f.ReadAllText("bigConfig.xml")).Returns(_MultiEntryXml);
      mWriter = new ConfigWriter(mFile.Object, "config.xml");
      mBigWriter = new ConfigWriter(mFile.Object, "bigConfig.xml");
    }

    private Mock<IFile> mFile;
    private ConfigWriter mWriter;
    private ConfigWriter mBigWriter;

    private const string _SingleEntryXml =
      @"<?xml version=""1.0"" encoding=""utf-8""?>
<configuration>
  <appSettings>
    <add key=""name"" value=""joe"" />
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