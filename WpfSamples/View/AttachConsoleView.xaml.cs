using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace WpfSamples.View
{
    public partial class AttachConsoleView : Window
    {
        public AttachConsoleView()
        {
            InitializeComponent();

            var cw = new ConsoleWrapper();
            cw.AttachConsole();

            Console.WriteLine("aaaaaaaaaaaa");
        }
    }

    public class ConsoleWrapper
    {
        public uint ConsoleProcId { get; set; }

        public void AttachConsole()
        {
            if (GetConsoleWindow().ToInt32() == 0)
                if (!AllocConsole())
                    throw new ApplicationException(GetMessage);
        }

        #region WinAPI stuff

        private const int FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
        private const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll", EntryPoint = "FormatMessageW", CharSet = CharSet.Unicode)]
        private static extern int FormatMessage(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId,
            StringBuilder lpBuffer, int nSize, IntPtr vaListArguments);

        private static string GetMessage
        {
            get
            {
                var errorCode = Marshal.GetLastWin32Error();
                var message = new StringBuilder(1025);
                var res = FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, errorCode, 0, message, message.Capacity,
                    IntPtr.Zero);
                return string.Format("Win32 Error 0x{0}{1}", errorCode.ToString("X"), res == 0 ? string.Empty : ": " + message);
            }
        }

        #endregion
    }
}