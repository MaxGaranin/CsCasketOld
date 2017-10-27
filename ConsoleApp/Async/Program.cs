using System;
using System.IO;

namespace ConsoleApp.Async
{
    internal class Program
    {
        private static void Main()
        {
//            var worker = new CancelTaskWorker();
//            worker.StartWithTasks();

//            var worker2 = new CancelAsyncWorker();
//            worker2.Work();

//            const string uri =
//                "http://as21470.samnipineft.ru:8010/repository/download/Tsfm2_DevelopMaster/7475:id/TSFM2-1.2.5.3397.dev.zip";

            const string uri =
                "http://as21470.samnipineft.ru:8088/Application%20Files/postgresql-9.4.1-3-windows-x64.exe";

//            const string uri =
//                "https://download.jetbrains.com/resharper/JetBrains.ReSharperUltimate.2017.2.2.exe";

//            const string uri =
//                "https://github.com/MaxGaranin/WpfSamples/archive/master.zip";

            var path = Path.Combine(@"d:\", Path.GetFileName(uri));

            FileDownloader.Download(uri, path);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}