using System.Diagnostics;
using System.Reflection;

namespace ConsoleApp.SelfDelete
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var info = new ProcessStartInfo();
            info.Arguments = "/C choice /C Y /N /D Y /T 1 & del " + Assembly.GetExecutingAssembly().Location;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            Process.Start(info);
        }
    }
}