using System.Runtime.InteropServices;

namespace ConsoleApp.Hardware
{
    public class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MemoryStatusEx
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MemoryStatusEx()
            {
                this.dwLength = (uint) Marshal.SizeOf(typeof(MemoryStatusEx));
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatusEx lpBuffer);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicallyInstalledSystemMemory(out ulong totalMemoryInKilobytes);
    }
}