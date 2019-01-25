using System.Threading.Tasks;

namespace ConsoleApp.Async.ThreadsSynchronization
{
    public class Garden
    {
        public static void Main(string[] args)
        {

        }

        private int _m;
        private int _n;

        public Garden(int n, int m)
        {
            _n = n;
            _m = m;
        }

        public void Work()
        {
            var task1 = Task.Run(() => { ExecuteGardener(1); });
            var task2 = Task.Run(() => { ExecuteGardener(2); });

            Task.WaitAll(task1, task2);
        }

        private void ExecuteGardener(int gardenerIndex)
        {

        }
    }
}