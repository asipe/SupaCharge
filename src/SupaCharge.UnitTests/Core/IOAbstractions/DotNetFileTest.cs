using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.ExceptionHandling;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Core.ThreadingAbstractions;
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
    public void TestOpenWithAccess() {
      using (var strm = mFile.Open(mPath, FileMode.Open, FileAccess.Read))
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
    public void TestWriteAllLines() {
      File.Delete(mPath);
      mFile.WriteAllLines(mPath, "one", "two", "three");
      Assert.That(File.ReadAllLines(mPath), Is.EqualTo(BA("one", "two", "three")));
    }

    [Test]
    public void TestCopy() {
      var dest = mPath + ".1";
      mFile.Copy(mPath, dest);
      Assert.That(File.ReadAllText(mPath), Is.EqualTo("data"));
    }

    [Test]
    public void TestCopyOverwriteTrue() {
      var dest = mPath + ".1";
      mFile.Copy(mPath, dest);
      mFile.Copy(mPath, dest, true);
      Assert.That(File.ReadAllText(mPath), Is.EqualTo("data"));
    }

    [Test]
    public void TestCopyOverwriteFalse() {
      var dest = mPath + ".1";
      mFile.Copy(mPath, dest);
      var ex = Assert.Throws<IOException>(() => mFile.Copy(mPath, dest, false));
      Assert.That(ex.Message, Is.StringStarting("The file").And.StringEnding("already exists."));
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

    [TestCase(0)]
    [TestCase(100)]
    public void TestDeleteWithWaitWhenNoWaitNecessary(int waitMilliseconds) {
      mFile.Delete(mPath, waitMilliseconds);
      Assert.That(mFile.Exists(mPath), Is.False);
    }

    [Test]
    public void TestDeleteWithWaitWhenUnableToDeleteInTimeThrows() {
      using (var strm = File.Open(mPath, FileMode.Open, FileAccess.Read, FileShare.Delete)) {
        var ex = Assert.Throws<SupaChargeException>(() => mFile.Delete(mPath, 100));
        Assert.That(ex.Message, Is.StringMatching("Unable to verify delete of .+abc.txt in 100ms"));
        Assert.That(mFile.Exists(mPath), Is.True);
        strm.Close();
      }
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(5)]
    public void TestDeleteWithWaitWhenAbleToDeleteInTime(int readerCount) {
      var batch = new WorkQueueBatch(new ThreadPoolWorkQueue());
      var random = new Random();
      var execArgs = Enumerable
        .Range(0, readerCount)
        .Select(x => new {
                           Evt = new ManualResetEvent(false),
                           Delay = random.Next(500)
                         })
        .ToArray();

      Array.ForEach(execArgs, a => batch.Add(a, args => {
                                                  using (var strm = File.Open(mPath, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.Read)) {
                                                    args.Evt.Set();
                                                    Thread.Sleep(args.Delay);
                                                    strm.Close();
                                                  }
                                                }));
      Array.ForEach(execArgs, a => Assert.That(a.Evt.WaitOne(1000), Is.True));
      var sw = Stopwatch.StartNew();
      mFile.Delete(mPath, 1000);
      batch.WaitAll(1000);
      sw.Stop();
      var min = execArgs.Min(a => a.Delay);
      Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(min - 10).And.LessThan(1000));
      Assert.That(mFile.Exists(mPath), Is.False);
    }

    [Test]
    public void TestSize() {
      Assert.That(mFile.GetSize(mPath), Is.EqualTo(4));
      File.WriteAllText(mPath, "");
      Assert.That(mFile.GetSize(mPath), Is.EqualTo(0));
    }

    [Test]
    public void TestSizeFileDoesNotExist() {
      Assert.Throws<FileNotFoundException>(() => mFile.GetSize(mPath + "_"));
    }

    [Test]
    public void TestMove() {
      mFile.Move(mPath, mPath + ".1");
      Assert.That(mFile.Exists(mPath), Is.False);
      Assert.That(mFile.ReadAllText(mPath + ".1"), Is.EqualTo("data"));
    }

    [Test]
    public void TestReadAllTextTrimmed() {
      Assert.That(mFile.ReadAllTextTrimmed(mPath), Is.EqualTo("data"));
      mFile.WriteAllLines(mPath, "1", "2", "", "   ", "3  ", "   4  ");
      Assert.That(mFile.ReadAllTextTrimmed(mPath), Is.EqualTo("1234"));
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mFile = new DotNetFile();
      mPath = Path.Combine(TempDir, "abc.txt");
      File.WriteAllText(mPath, "data");
    }

    [TearDown]
    public void DoTearDown() {
      new DotNetFile().Delete(mPath, 5000);
    }

    private DotNetFile mFile;
    private string mPath;
  }
}