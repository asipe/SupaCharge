using System.IO;
using System.Text;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.IOAbstractions {
  [TestFixture]
  public class DotNetFileTest : BaseTestCase {
    [Test]
    public void TestOpen() {
      using (var strm = mFile.Open(mPath, FileMode.Open))
      using (var rdr = new StreamReader(strm))
        Assert.That(rdr.ReadToEnd(), Is.EqualTo("data"));
    }

    [Test]
    public void TestReadAllText() {
      Assert.That(mFile.ReadAllText(mPath), Is.EqualTo("data"));
    }

    [Test]
    public void TestReadAllLines() {
      Assert.That(mFile.ReadAllLines(mPath)[0], Is.EqualTo("data"));
      Assert.That(mFile.ReadAllLines(mPath).Length, Is.EqualTo(1));
    }

    [Test]
    public void TestOpenText() {
      using (var rdr = mFile.OpenText(mPath))
        Assert.That(rdr.ReadToEnd(), Is.EqualTo("data"));
    }

    [Test]
    public void TestReadAllBytes() {
      Assert.That(mFile.ReadAllBytes(mPath), Is.EqualTo(Encoding.UTF8.GetBytes("data")));
    }

    [Test]
    public void TestWriteAllBytes() {
      File.Delete(mPath);
      mFile.WriteAllBytes(mPath, Encoding.UTF8.GetBytes("data"));
      Assert.That(File.ReadAllText(mPath), Is.EqualTo("data"));
    }

    [Test]
    public void TestWriteAllText() {
      File.Delete(mPath);
      mFile.WriteAllText(mPath, "data");
      Assert.That(File.ReadAllText(mPath), Is.EqualTo("data"));
    }

    [Test]
    public void TestCopy() {
      var dest = mPath + ".1";
      mFile.Copy(mPath, dest);
      Assert.That(File.ReadAllText(mPath), Is.EqualTo("data"));
    }

    [Test]
    public void TestExists() {
      Assert.That(mFile.Exists(mPath + ".tmp"), Is.False);
      Assert.That(mFile.Exists(mPath), Is.True);
    }

    [Test]
    public void TestDelete() {
      mFile.Delete(mPath);
      Assert.That(mFile.Exists(mPath), Is.False);
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mFile = new DotNetFile();
      mPath = Path.Combine(TempDir, "abc.txt");
      File.WriteAllText(mPath, "data");
    }

    private DotNetFile mFile;
    private string mPath;
  }
}