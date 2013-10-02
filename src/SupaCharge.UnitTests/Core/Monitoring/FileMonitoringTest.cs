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

      WriteAnEntryToTempDirFile();

      new Retry(100, 50)
        .WithWork(m => Assert.That(seen.Select(c => c.FileName), Is.EqualTo(BA("file1.txt"))))
        .Start();

      mMonitor.Stop();
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mFile1Path = Path.Combine(TempDir, "file1.txt");
      File.WriteAllText(mFile1Path, "file.txt");
      mMonitor = new DirMonitor(TempDir);
    }

    private void WriteAnEntryToTempDirFile() {
      using (var strm = File.OpenWrite(mFile1Path)) {
        var buf = Encoding.ASCII.GetBytes("Hello");
        strm.Write(buf, 0, buf.Length);
        strm.Close();
      }
    }

    private string mFile1Path;
    private DirMonitor mMonitor;
  }
}