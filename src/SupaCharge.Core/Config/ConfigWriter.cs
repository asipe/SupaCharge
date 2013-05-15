using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using SupaCharge.Core.IOAbstractions;

namespace SupaCharge.Core.Config {
  public class ConfigWriter {
    public ConfigWriter(IFile file, string filePath) {
      mDoc = XDocument.Parse(file.ReadAllText(filePath));
      mFile = file;
      mPath = filePath;
    }

    public void Save() {
      using (var writer = new StringWriter()) {
        mDoc.Save(writer);
        mFile.WriteAllText(mPath, writer.ToString());
        writer.Close();
      }
    }

    public void Set(string key, string value) {
      var node = mDoc.XPathSelectElement(BuildQuery(key));
      node.Attribute("value").Value = value;
    }

    private string BuildQuery(string key) {
      return string.Format("//configuration/appSettings/add[@key='{0}']", key);
    }

    private readonly XDocument mDoc;
    private readonly IFile mFile;
    private readonly string mPath;
  }
}