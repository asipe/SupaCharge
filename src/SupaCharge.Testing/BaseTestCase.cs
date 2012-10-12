using System;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SupaCharge.Testing {
  public abstract class BaseTestCase {
    protected Fixture ObjectFixture{get;private set;}
    protected MockFactory MokFac{get;private set;}
    protected string TempDir{get;private set;}

    [SetUp]
    public void BaseSetup() {
      MokFac = new MockFactory(MockBehavior.Strict);
      ObjectFixture = new Fixture();
    }

    [TearDown]
    public void BaseTearDown() {
      CleanTempDirectory();
      VerifyMocks();
    }

    private void VerifyMocks() {
      if (TestContext.CurrentContext.Result.Status == TestStatus.Passed)
        MokFac.VerifyAll();
    }

    private void CleanTempDirectory() {
      if (TempDir != null && Directory.Exists(TempDir))
        Directory.Delete(TempDir, true);
    }

    protected Mock<T> Mok<T>() where T : class {
      return MokFac.Create<T>();
    }

    protected T[] CM<T>() {
      return CM<T>(3);
    }

    protected T[] CM<T>(int number) {
      return ObjectFixture.CreateMany<T>(number).ToArray();
    }

    protected T CA<T>() {
      return ObjectFixture.CreateAnonymous<T>();
    }

    protected T[] BA<T>(params T[] objects) {
      return objects;
    }

    protected string CreateTempDir() {
      TempDir = Guid.NewGuid().ToString("N");
      Directory.CreateDirectory(TempDir);
      return TempDir;
    }
  }
}