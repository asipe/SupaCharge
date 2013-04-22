using Moq;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.IOAbstractions {
  [TestFixture]
  public class DynamicPathTest : BaseTestCase {
    [Test]
    public void TestOriginalPathDefaultsToCorrectVal() {
      Assert.That(Init(@"c:\apps\myapp\data\").OriginalPath, Is.EqualTo(@"c:\apps\myapp\data\"));
    }

    [Test]
    public void TestDefaultCurrentPathEqualsOriginalPath() {
      Assert.That(Init(@"\\10.1.1.1\d$\myapp\data\").CurrentPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
    }

    [TestCase(@"c:\apps\myapp\data\")]
    [TestCase(@"apps\myapp\data\yo.txt")]
    [TestCase(@"c:\apps\myapp\data\yo.txt")]
    [TestCase(@"c:\apps\my231app\data\")]
    [TestCase(@"\\10.1.1.1\d$\myapp\data\")]
    public void TestNonDnsCurrentPathsRefreshesToOriginalPath(string path) {
      var rPath = Init(path);
      rPath.Refresh();
      Assert.That(rPath.CurrentPath, Is.EqualTo(rPath.OriginalPath));
    }

    [Test]
    public void TestCurrentPathUsesAnIPAddressAfterBeingRefreshed() {
      var path = Init(@"\\zbeast\d$\myapp\data");
      mDns.Setup(i => i.GetIPAddress("zbeast")).Returns("12.345.67.890");
     
      path.Refresh();
      
      Assert.That(path.CurrentPath, Is.EqualTo(@"\\12.345.67.890\d$\myapp\data"));
    }

    [SetUp]
    public void DoSetup() {
      mDns = Mok<IDns>();
    }

    private DynamicPath Init(string path) {
      return new DynamicPath(path, mDns.Object);
    }

    private Mock<IDns> mDns;
  }
}