namespace ConsoleApp.Hardware
{
    public class HardwareInfoHelper
    {
        public static ulong GetMemorySize()
        {
            ulong installedMemory = 0;

            var memStatus = new NativeMethods.MemoryStatusEx();
            if (NativeMethods.GlobalMemoryStatusEx(memStatus))
            {
                installedMemory = memStatus.ullTotalPhys;
            }

            return installedMemory;
        }

        public static ulong GetMemorySizeInKb()
        {
            NativeMethods.GetPhysicallyInstalledSystemMemory(out var memKb);
            return memKb;
        }
    }
}