using System;
using System.ComponentModel;
using Moq;
using NUnit.Framework;

namespace ReplaceBackgroundWorker.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestCalculateMethod()
        {
            var asyncManagerMock = new Mock<IAsyncManager>();

            asyncManagerMock
                .Setup(
                    pa => pa.BackgroundTask(
                        It.IsAny<Action<DoWorkEventArgs>>(),
                        It.IsAny<Action<RunWorkerCompletedEventArgs>>(),
                        It.IsAny<Action<Exception>>()))
                .Callback<Action<DoWorkEventArgs>, Action<RunWorkerCompletedEventArgs>, Action<Exception>>(
                    (action, onCompleted, error) =>
                    {
                        if (action == null) return;

                        var workArgs = new DoWorkEventArgs(null);

                        try
                        {
                            action(workArgs);
                        }
                        catch (Exception ex)
                        {
                            error(ex);
                        }

                        if (onCompleted != null)
                            onCompleted(new RunWorkerCompletedEventArgs(workArgs.Result, null, false));
                    });

            var calculator = new Calculator(asyncManagerMock.Object);
            calculator.Calculate();
        }
    }
}