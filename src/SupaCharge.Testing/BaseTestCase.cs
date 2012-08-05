using System.Linq;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SupaCharge.Testing {
  public abstract class BaseTestCase {
    protected Fixture ObjectFixture{get;private set;}
    protected MockFactory MokFac{get;private set;}

    [SetUp]
    public void BaseSetup() {
      MokFac = new MockFactory(MockBehavior.Strict);
      ObjectFixture = new Fixture();
    }

    [TearDown]
    public void BaseTearDown() {
      if (TestContext.CurrentContext.Result.Status == TestStatus.Passed)
        MokFac.VerifyAll();
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
  }
}