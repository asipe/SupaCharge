using System;
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
      }
    }

    public void Set(string nodeToChange, string newValue) {
      var node = mDoc.XPathSelectElement("//configuration/appSettings/add");
      node.Attribute(nodeToChange).Value = newValue;
    }

    private readonly XDocument mDoc;
    private readonly IFile mFile;
    private readonly string mPath;
  }
}