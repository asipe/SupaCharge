using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using SupaCharge.Core.IOAbstractions;

namespace SupaCharge.Core.Config {
  public class ConfigWriter {
    private class NullEncodeStrWriter : StringWriter {
      public override Encoding Encoding {
        get { return null; }
      }
    }

    public ConfigWriter(IFile file, string filePath) {
      mDoc = XDocument.Parse(file.ReadAllText(filePath));
      mDoc.Declaration = new XDeclaration("1.0", null, null);
      mFile = file;
      mPath = filePath;
    }

    public void Save() {
      using (var strWriter = new NullEncodeStrWriter()) {
        mDoc.Save(strWriter);
        mFile.WriteAllText(mPath, strWriter.ToString());
        strWriter.Close();
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