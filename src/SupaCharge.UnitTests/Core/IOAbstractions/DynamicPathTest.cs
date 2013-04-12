using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.IOAbstractions {
  [TestFixture]
  public class DynamicPathTest : BaseTestCase {
    [Test]
    public void TestOriginalPathsReturnCorrectValue() {
      Assert.That(mLocalPath.OriginalPath, Is.EqualTo(@"c:\apps\myapp\data\"));
      Assert.That(mTcpUncPath.OriginalPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
      Assert.That(mHostNameUncPath.OriginalPath, Is.EqualTo(@"\\zbeast\d$\myapp\data"));
    }

    [Test]
    public void TestCurrentPathIsCorrectInit() {
      Assert.That(mLocalPath.CurrentPath, Is.EqualTo(@"c:\apps\myapp\data\"));
      Assert.That(mTcpUncPath.CurrentPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
      Assert.That(mHostNameUncPath.CurrentPath, Is.EqualTo(@"\\zbeast\d$\myapp\data"));
    }

    [Test]
    public void TestCurrentPathEqualsOriginalPathOnRefreshedLocalPath() {
      Assert.That(mLocalPath.OriginalPath, Is.EqualTo(@"c:\apps\myapp\data\"));
      Assert.That(mLocalPath.CurrentPath, Is.EqualTo(@"c:\apps\myapp\data\"));
      mLocalPath.Refresh();
      Assert.That(mLocalPath.CurrentPath, Is.EqualTo(@"c:\apps\myapp\data\"));
    }

    [Test]
    public void TestCurrentPathEqualsOriginalPathOnRefreshedTcpUncPath() {
      Assert.That(mTcpUncPath.OriginalPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
      Assert.That(mTcpUncPath.CurrentPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
      mLocalPath.Refresh();
      Assert.That(mTcpUncPath.CurrentPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
    }

    [SetUp]
    public void DoSetup() {
      mLocalPath = new DynamicPath(@"c:\apps\myapp\data\");
      mTcpUncPath = new DynamicPath(@"\\10.1.1.1\d$\myapp\data\");
      mHostNameUncPath = new DynamicPath(@"\\zbeast\d$\myapp\data");
    }

    private DynamicPath mLocalPath;
    private DynamicPath mTcpUncPath;
    private DynamicPath mHostNameUncPath;
  }
}