using System.IO;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.IOAbstractions {
  [TestFixture]
  public class DotNetDirectoryTest : BaseTestCase {
    [Test]
    public void TestGetCurrentDirectory() {
      Assert.That(mDir.GetCurrentDirectory(), Is.EqualTo(Directory.GetCurrentDirectory()));
    }

    [Test]
    public void TestGetFiles() {
      Assert.That(mDir.GetFiles(TempDir, "*.*"), Is.Empty);
      var path = Path.Combine(TempDir, "abc.txt");
      File.WriteAllText(path, "data");
      Assert.That(mDir.GetFiles(TempDir, "*.txt").Length, Is.EqualTo(1));
      Assert.That(mDir.GetFiles(TempDir, "*.txt")[0], Is.StringEnding("abc.txt"));
    }

    [Test]
    public void TestCreateDirectory() {
      var path = Path.Combine(TempDir, "adir");
      var info = mDir.CreateDirectory(path);
      Assert.That(Directory.Exists(path), Is.True);
      Assert.That(info.Exists, Is.True);
    }

    [Test]
    public void TestExists() {
      Assert.That(mDir.Exists(TempDir), Is.True);
      Assert.That(mDir.Exists("abc"), Is.False);
    }

    [Test]
    public void TestGetDirectories() {
      Assert.That(mDir.GetDirectories(TempDir), Is.Empty);
      mDir.CreateDirectory(Path.Combine(TempDir, "a"));
      mDir.CreateDirectory(Path.Combine(TempDir, "b"));
      Assert.That(mDir.GetDirectories(TempDir), Is.EqualTo(BA(Path.Combine(TempDir, "a"), Path.Combine(TempDir, "b"))));
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mDir = new DotNetDirectory();
    }

    private DotNetDirectory mDir;
  }
}