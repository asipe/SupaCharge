using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SupaCharge.Core.Patterns;
using SupaCharge.Testing;

namespace SupaCharge.UnitTests.Patterns {
  [TestFixture]
  public class PipelineTest : BaseTestCase {
    [Test]
    public void TestExecuteWithNoStagesDoesNothing() {
      InitPipeline();
      mPipeline.Execute(44);
    }

    [Test]
    public void TestExecuteWithSingleStage() {
      var stage = Mok<IStage<int>>();
      stage.Setup(s => s.Execute(It.Is<CancelToken>(t => !t.Cancelled), 44));
      InitPipeline(stage.Object);
      mPipeline.Execute(44);
    }

    [Test]
    public void TestExecuteWithMultipleStages() {
      var stages = BA(Mok<IStage<int>>(), Mok<IStage<int>>(), Mok<IStage<int>>());
      Array.ForEach(stages, stage => stage.Setup(s => s.Execute(It.Is<CancelToken>(t => !t.Cancelled), 44)));
      InitPipeline(stages.Select(s => s.Object).ToArray());
      mPipeline.Execute(44);
    }

    [Test]
    public void TestExecuteWithMultipleStagesWhenOneCancelsToken() {
      var stages = BA(Mok<IStage<int>>(), Mok<IStage<int>>(), Mok<IStage<int>>());
      stages[0].Setup(s => s.Execute(It.Is<CancelToken>(t => !t.Cancelled), 44));
      stages[1]
        .Setup(s => s.Execute(It.Is<CancelToken>(t => !t.Cancelled), 44))
        .Callback<CancelToken, int>((c, x) => c.Cancel());
      InitPipeline(stages.Select(s => s.Object).ToArray());
      mPipeline.Execute(44);
    }

    [SetUp]
    public void DoSetup() {
      mPipeline = null;
    }

    private Pipeline<int> mPipeline;

    private void InitPipeline(params IStage<int>[] stages) {
      mPipeline = new Pipeline<int>(stages);
    }
  }
}