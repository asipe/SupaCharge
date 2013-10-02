using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SupaCharge.Core.Monitoring;
using SupaCharge.Core.ThreadingAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.Monitoring {
  [TestFixture]
  public class FileMonitoringTest : BaseTestCase {
    [Test]
    public void TestChangesToAFileResultInChangeEventBeingRaised() {
      var seen = new List<ChangedEvent>();
      mMonitor.OnFileChange += (o, a) => seen.Add(a);
      mMonitor.Start();
      WriteAnEntryToSpecifiedFile(mFilePath);

      new Retry(100, 50)
        .WithWork(m => Assert.That(seen.Select(c => c.FileName), Is.EqualTo(BA(mFilePath))))
        .Start();

      mMonitor.Stop();
    }

    [Test]
    public void TestChangesToAFileAreRaisedInSubdirectories() {
      var seen = new List<ChangedEvent>();

      var subDir = Path.Combine(TempDir, "subDir");
      Directory.CreateDirectory(subDir);
      Assert.That(Directory.Exists(subDir));
      var subPath = Path.Combine(subDir, "file2.txt");
      File.WriteAllText(subPath, "file");
      subPath = Path.GetFullPath(subPath);

      mMonitor.OnFileChange += (o, a) => seen.Add(a);
      mMonitor.Start();

      WriteAnEntryToSpecifiedFile(subPath);

      new Retry(100, 50)
        .WithWork(m => Assert.That(seen.Select(c => c.FileName), Is.EqualTo(BA(subPath))))
        .Start();

      mMonitor.Stop();
    }

    [Test]
    public void TestDeletingAFileDoesNotRaiseAChangEvent() {
      var seen = new List<ChangedEvent>();
      mMonitor.OnFileChange += (o, a) => seen.Add(a);
      mMonitor.Start();

      File.Delete(mFilePath);

      new Retry(100, 50)
        .WithWork(m => Assert.IsEmpty(seen))
        .Start();

      mMonitor.Stop();
    }

    [Test]
    public void TestRenamingDoesNotRaiseAChangeEvent() {
      var seen = new List<ChangedEvent>();
      mMonitor.OnFileChange += (o, a) => seen.Add(a);
      mMonitor.Start();

      File.Move(mFilePath, Path.Combine(TempDir, "newFile"));
      Assert.False(File.Exists(mFilePath));

      new Retry(100, 50)
        .WithWork(m => Assert.IsEmpty(seen))
        .Start();

      mMonitor.Stop();
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mFilePath = Path.Combine(TempDir, "file1.txt");
      File.WriteAllText(mFilePath, "file.txt");
      mFilePath = Path.GetFullPath(mFilePath);
      mMonitor = new DirMonitor(TempDir);
    }

    private void WriteAnEntryToSpecifiedFile(string fileToWriteTo) {
      using (var strm = File.OpenWrite(fileToWriteTo)) {
        var buf = Encoding.ASCII.GetBytes("Hello");
        strm.Write(buf, 0, buf.Length);
        strm.Close();
      }
    }

    private string mFilePath;
    private DirMonitor mMonitor;
  }
}