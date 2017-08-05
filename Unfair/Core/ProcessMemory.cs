using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Unfair.Core
{
    /// <summary>
    /// Provides memory reading/writing functionality for a process.
    /// </summary>
    public class ProcessMemory
    {
        public Process Process { get; set; }

        private const int ProcessAllAccess = 0x0010;

        public bool Attach(string processName)
        {
            Process = Process.GetProcessesByName(processName).FirstOrDefault();

            if (Process == null)
                return false;

            return true;
        }

        public ProcessModule GetModule(string moduleName)
        {
            foreach (ProcessModule module in Process.Modules)
            {
                if (module.ModuleName == moduleName)
                    return module;
            }
            return null;
        }

        public T Read<T>(int address) where T : struct
        {
            byte[] value = new byte[Marshal.SizeOf(typeof(T))];
            ReadProcessMemory(Process.Handle.ToInt32(), address, value, value.Length, 0);
            GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            T result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return result;
        }

        public int ReadInt32(int address)
        {
            byte[] value = new byte[sizeof(int)];
            ReadProcessMemory(Process.Handle.ToInt32(), address, value, value.Length, 0);
            return BitConverter.ToInt32(value, 0);
        }

        public float ReadFloat(int address)
        {
            byte[] value = new byte[sizeof(float)];
            ReadProcessMemory(Process.Handle.ToInt32(), address, value, value.Length, 0);
            return BitConverter.ToSingle(value, 0);
        }

        public bool ReadBool(int address)
        {
            byte[] value = new byte[sizeof(bool)];
            ReadProcessMemory(Process.Handle.ToInt32(), address, value, value.Length, 0);
            return BitConverter.ToBoolean(value, 0);
        }

        public void WriteFloat(int address, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteProcessMemory(Process.Handle.ToInt32(), address, bytes, bytes.Length, 0);
        }

        public void WriteBool(int address, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            WriteProcessMemory(Process.Handle.ToInt32(), address, bytes, bytes.Length, 0);
        }

        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesWritten);
    }
}