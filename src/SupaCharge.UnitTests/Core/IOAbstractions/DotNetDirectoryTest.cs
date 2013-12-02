using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Core.ThreadingAbstractions;
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
      Assert.That(mDir.GetFiles(TempDir, "*.txt"), Is.EqualTo(BA(path)));
    }

    [Test]
    public void TestGetFilesSearchOption() {
      Assert.That(mDir.GetFiles(TempDir, "*.*", SearchOption.AllDirectories), Is.Empty);
      Assert.That(mDir.GetFiles(TempDir, "*.*", SearchOption.TopDirectoryOnly), Is.Empty);
      var path = Path.Combine(TempDir, "abc.txt");
      File.WriteAllText(path, "data");
      Assert.That(mDir.GetFiles(TempDir, "*.txt", SearchOption.AllDirectories), Is.EqualTo(BA(path)));
      Assert.That(mDir.GetFiles(TempDir, "*.txt", SearchOption.TopDirectoryOnly), Is.EqualTo(BA(path)));
      var dir = Path.Combine(TempDir, "dir1");
      Directory.CreateDirectory(dir);
      var path2 = Path.Combine(dir, "abc.txt");
      File.WriteAllText(path2, "data");
      Assert.That(mDir.GetFiles(TempDir, "*.txt", SearchOption.AllDirectories), Is.EquivalentTo(BA(path, path2)));
      Assert.That(mDir.GetFiles(TempDir, "*.txt", SearchOption.TopDirectoryOnly), Is.EqualTo(BA(path)));
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

    [Test]
    public void TestDeleteDirectory() {
      mDir.Delete(TempDir);
      Assert.That(mDir.Exists(TempDir), Is.False);
    }

    [Test]
    public void TestDeleteDirectoryRecursive() {
      mDir.CreateDirectory(Path.Combine(TempDir, "a"));
      mDir.CreateDirectory(Path.Combine(TempDir, "b"));
      mDir.Delete(TempDir, true);
      Assert.That(mDir.Exists(TempDir), Is.False);
    }

    [TestCase(0)]
    [TestCase(100)]
    public void TestDeleteDirectoryWithRetrySuccess(int waitMilliseconds) {
      var dir = Path.Combine(TempDir, "a");
      mDir.CreateDirectory(dir);
      mDir.Delete(dir, waitMilliseconds);
      Assert.That(mDir.Exists(dir), Is.False);
    }

    [TestCase(0)]
    [TestCase(25)]
    public void TestDeleteDirectoryWithRetryUnableToDeleteThrows(int waitMilliseconds) {
      var dir = Path.Combine(TempDir, "a");
      var file = Path.Combine(dir, "a.txt");
      mDir.CreateDirectory(dir);

      using (var strm = File.Create(file)) {
        var sw = Stopwatch.StartNew();
        var ex = Assert.Throws<IOException>(() => mDir.Delete(dir, waitMilliseconds));
        sw.Stop();
        Assert.That(ex.Message, Is.EqualTo("The process cannot access the file 'a.txt' because it is being used by another process."));
        Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(waitMilliseconds).And.LessThan(250));
        strm.Close();
      }
    }

    [Test]
    public void TestDeleteDirectoryWithRetryWithFileShareDeleteFlagUnableToDeleteThrows() {
      var dir = Path.Combine(TempDir, "a");
      var file = Path.Combine(dir, "a.txt");
      mDir.CreateDirectory(dir);
      File.WriteAllText(file, "data");

      using (var strm = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite)) {
        var sw = Stopwatch.StartNew();
        var ex = Assert.Throws<UnauthorizedAccessException>(() => mDir.Delete(dir, 100));
        sw.Stop();
        Assert.That(ex.Message, Is.EqualTo("Access to the path 'a.txt' is denied."));
        Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(100).And.LessThan(250));
        strm.Close();
      }
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(5)]
    public void TestDeleteWithWaitWhenAbleToDeleteInTime(int readerCount) {
      var dir = Path.Combine(TempDir, "a");
      var file = Path.Combine(dir, "a.txt");
      mDir.CreateDirectory(dir);
      File.WriteAllText(file, "data");

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
                                                  using (var strm = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Delete | FileShare.ReadWrite)) {
                                                    args.Evt.Set();
                                                    Thread.Sleep(args.Delay);
                                                    strm.Close();
                                                  }
                                                }));
      Array.ForEach(execArgs, a => Assert.That(a.Evt.WaitOne(1000), Is.True));
      var sw = Stopwatch.StartNew();
      mDir.Delete(dir, 1000);
      batch.Wait(1000);
      sw.Stop();
      var min = execArgs.Min(a => a.Delay);
      Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(min - 10).And.LessThan(1000));
      Assert.That(mDir.Exists(dir), Is.False);
    }

    [SetUp]
    public void DoSetup() {
      CreateTempDir();
      mDir = new DotNetDirectory();
    }

    [TearDown]
    public void DoTearDown() {
      if ((TempDir != null) && mDir.Exists(TempDir))
        mDir.Delete(TempDir, 500);
    }

    private DotNetDirectory mDir;
  }
}