using System;
using System.Runtime.InteropServices;

namespace ExandasOracle.Native
{
    static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern bool LockWindowUpdate(IntPtr hWndLock);

        public const int HWND_BROADCAST = 0xffff;
        public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll")]
        public static extern int RegisterWindowMessage(string message);
    }
}
