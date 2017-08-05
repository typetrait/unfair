using System.Runtime.InteropServices;

namespace Unfair.Core
{
    /// <summary>
    /// The WinAPI GDI+ rectangle struct.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left, top, right, bottom;
    }
}