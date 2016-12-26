using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests45
{
    /// <summary>
    /// Описание проблемы на страничке
    /// http://stackoverflow.com/questions/26798845/await-task-delay-vs-task-delay-wait
    /// </summary>
    [TestFixture]
    public class AsyncDelayTests
    {
        [Test]
        public void TestWait()
        {
            var t = Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("Start");
                Task.Delay(3000).Wait();
                Debug.WriteLine("Done");
            });
            t.Wait();
            Debug.WriteLine("All done");
        }

        [Test]
        public async Task TestAwait() //Note the return type of Task. This is required to get the async test 'waitable' by the framework
        {
//            await Task.Factory.StartNew(async () =>
//            {
//                Debug.WriteLine("Start");
//                await Task.Delay(3000);
//                Debug.WriteLine("Done");
//            }).Unwrap();    //Note the call to Unwrap. This automatically attempts to find the most Inner `Task` in the return type.
//            Debug.WriteLine("All done");

            // Этот способ лучше
            await Task.Run(async () => //Task.Run automatically unwraps nested Task types!
            {
                Debug.WriteLine("Start");
                await Task.Delay(5000);
                Debug.WriteLine("Done");
            });
            Debug.WriteLine("All done");
        }
    }
}