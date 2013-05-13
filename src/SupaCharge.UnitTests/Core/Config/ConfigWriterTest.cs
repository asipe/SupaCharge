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
    public void TestSaveUnchangedConfigSavesSameXMLWithMultipleEntries() {
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
      mWriter.Set("value", "bob");
      mWriter.Save();
      mFile.Verify(f => f.WriteAllText("config.xml", It.Is<string>(s => XDocument
                                                                          .Parse(s)
                                                                          .XPathSelectElement("//configuration/appSettings/add")
                                                                          .Attribute("value")
                                                                          .Value == "bob")));
    }

    //var writer  =  new Writer(..., 'abc.xml')
    //writer.set("aac_leave_on_exit", "true");
    //writer.set("aac_ppg_portz", "1234");
    //writer.Commit()
    [SetUp]
    public void DoSetup() {
      mFile = Mok<IFile>();
      mFile.Setup(f => f.ReadAllText("config.xml")).Returns(_SingleEntryXml);
      mWriter = new ConfigWriter(mFile.Object, "config.xml");
    }

    private Mock<IFile> mFile;
    private ConfigWriter mWriter;

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
    <add key=""name"" value=""joe"" />
    <add key=""name"" value=""bob"" />
    <add key=""name"" value=""hal"" />
  </appSettings>
</configuration>";
  }
}