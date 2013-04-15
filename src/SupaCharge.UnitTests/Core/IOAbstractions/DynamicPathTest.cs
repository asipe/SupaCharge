using Moq;
using NUnit.Framework;
using SupaCharge.Core.IOAbstractions;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Core.IOAbstractions {
  [TestFixture]
  public class DynamicPathTest : BaseTestCase {
    [Test]
    public void TestDefaultOriginalPathIsCorrect() {
      Assert.That(Init(@"c:\apps\myapp\data\").OriginalPath, Is.EqualTo(@"c:\apps\myapp\data\"));
    }

    [Test]
    public void TestCurrentPathDefaultEqualsOriginalPath() {
      Assert.That(Init(@"\\10.1.1.1\d$\myapp\data\").CurrentPath, Is.EqualTo(@"\\10.1.1.1\d$\myapp\data\"));
    }

    [TestCase(@"c:\apps\myapp\data\")]
    [TestCase(@"apps\myapp\data\yo.txt")]
    [TestCase(@"c:\apps\myapp\data\yo.txt")]
    [TestCase(@"c:\apps\my231app\data\")]
    [TestCase(@"\\10.1.1.1\d$\myapp\data\")]
    public void TestCurrentPathAfterRefreshEqualsOriginals(string path) {
      var rPath = Init(path);
      rPath.Refresh();
      Assert.That(rPath.CurrentPath, Is.EqualTo(rPath.OriginalPath));
    }

    [Test]
    public void TestRefreshingUncPathWithHostNameCallsIdns() {
      var path = Init(@"\\zbeast\d$\myapp\data");
      mDns.Setup(i => i.GetIPAddress("zbeast")).Returns("Refreshed");
      path.Refresh();
      Assert.That(path.CurrentPath, Is.EqualTo("Refreshed"));
    } 
    
    [Test]
    public void TestFindHostName() {
      var path = Init(@"\\zbeast\d$\myapp\data");
      var complexPath = Init(@"\\myfavorite231hostname\d$\myapp\data");

      mDns.Setup(i => i.GetIPAddress("zbeast")).Returns("");
      path.Refresh();

      mDns.Setup(i => i.GetIPAddress("myfavorite231hostname")).Returns("");
      complexPath.Refresh();
    }

    private DynamicPath Init(string path) {
      return new DynamicPath(path, mDns.Object);
    }

    [SetUp]
    public void DoSetup() {
      mDns = Mok<IDns>();
    }

    private Mock<IDns> mDns;
  }
}