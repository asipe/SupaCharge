using System;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SupaCharge.Testing {
  public abstract class BaseTestCase {
    protected Fixture ObjectFixture{get;private set;}
#if NET35
    protected MockFactory MokFac{get;private set;}
#else
    protected MockRepository MokFac {get; private set;}
#endif
    protected string TempDir{get;private set;}

    [SetUp]
    public void BaseSetup() {
#if NET35
      MokFac = new MockFactory(MockBehavior.Strict);
#else
      MokFac = new MockRepository(MockBehavior.Strict);
#endif
      ObjectFixture = new Fixture();
    }

    [TearDown]
    public void BaseTearDown() {
      CleanTempDirectory();
      VerifyMocks();
    }

    private void VerifyMocks() {
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
#if NET35
      return ObjectFixture.CreateAnonymous<T>();
#else
      return ObjectFixture.Create<T>();
#endif
    }

    protected T[] BA<T>(params T[] objects) {
      return objects;
    }

    protected string CreateTempDir() {
      TempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(TempDir);
      return TempDir;
    }
  }
}