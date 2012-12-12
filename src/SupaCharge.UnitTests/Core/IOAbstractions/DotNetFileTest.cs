using System.IO;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.IOAbstractions {
  [TestFixture]
  public class DotNetFileTest : BaseTestCase {
    [Test]
    public void TestOpen() {
      var path = Path.Combine(TempDir, "abc.txt");
      File.WriteAllText(path, "data");
      using (var strm = mFile.Open(path, FileMode.Open))
      using (var rdr = new StreamReader(strm))
        Assert.That(rdr.ReadToEnd(), Is.EqualTo("data"));
    }

    [Test]
    public void TestReadAllText() {
      var path = Path.Combine(TempDir, "abc.txt");
      File.WriteAllText(path, "data");
      Assert.That(mFile.ReadAllText(path), Is.EqualTo("data"));
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mFile = new DotNetFile();
    }

    private DotNetFile mFile;
  }
}