using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PublicUtilities
{
    public partial class NativeMethods
    {
        #region Captcha

        private const string CAPTCHADLL = @"libcheck.dll";

        [DllImport(CAPTCHADLL,EntryPoint="OpenImage_battlenet")]
        public extern static int GetCaptchaFromFile([MarshalAs(UnmanagedType.LPStr)]string filePath,
                                                        ref byte ch1, ref byte ch2, ref byte ch3, ref byte ch4,
                                                        ref byte ch5, ref byte ch6, ref byte ch7, ref byte ch8);  

        #endregion

        #region  API FILES

        private const string USER32 = "user32.dll";
        private const string USER = "User.dll";
        private const string KERNEL32 = "kernel32.dll";

        #endregion

        #region  API Structure

        public struct APIMsg
        {
            public int hwnd;
            public uint message;
            public int wParam;
            public long lParam;
            public uint time;
            public int pt;
        }

        #endregion

        #region  Action Message

        //ShowWindow参数
        public const int SW_SHOWNORMAL = 1;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWNOACTIVATE = 4;

        //SendMessage参数
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSCHAR = 0x106;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_CHAR = 0x102;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_CLICK = 0x00F5;

        public const uint LVM_FIRST = 0x1000;
        public const uint HDM_FIRST = 0x1200;
        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const uint LVM_GETITEMW = LVM_FIRST + 75;
        public const uint LVM_GETHEADER = LVM_FIRST + 31;
        public const uint HDM_GETITEMCOUNT = HDM_FIRST + 0;

        public const uint PROCESS_VM_OPERATION = 0x0008;
        public const uint PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;

        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RELEASE = 0x8000;

        public const uint MEM_RESERVE = 0x2000;
        public const uint PAGE_READWRITE = 4;

        #endregion

        #region  API Callback

        public delegate bool EnumWindowsCallBack(int hwnd, int lParam);

        #endregion

        #region  Window API

        [DllImport(USER32, EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(USER32, EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(USER32, EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        [DllImport(USER32, EntryPoint = "SetWindowPos")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);

        [DllImport(USER32, EntryPoint = "EnumWindows")]
        public static extern int EnumWindows(EnumWindowsCallBack x, int y);

        [DllImport(USER32, EntryPoint = "GetWindowTextW")]
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        [DllImport(USER32, EntryPoint = "GetClassNameW")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

        [DllImport(USER32, EntryPoint = "SetActiveWindow")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport(USER32, EntryPoint = "SetForegroundWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion

        #region    Message API

        [DllImport(USER32, CharSet = CharSet.Unicode, EntryPoint = "PostMessage")]
        public static extern bool PostMessage(IntPtr hWnd, uint message, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"> handle to destination window</param>
        /// <param name="Msg">message</param>
        /// <param name="wParam">first message parameter</param>
        /// <param name="lParam">second message parameter </param>
        /// <returns></returns>
        [DllImport(USER, EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        [DllImport(USER32, EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport(USER32, EntryPoint = "GetMessage")]
        public static extern int GetMessage(out APIMsg lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax);

        [DllImport(USER32, EntryPoint = "DispatchMessage")]
        public static extern int DispatchMessage(ref APIMsg lpMsg);

        [DllImport(USER32, EntryPoint = "TranslateMessage")]
        public static extern int TranslateMessage(ref APIMsg lpMsg);

        #endregion

        #region  Mouse

        [DllImport(USER32, EntryPoint = "SetCursorPos")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport(USER32, EntryPoint = "mouse_event")]
        public static extern void Mouse_Event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        #endregion

        #region  KeyBoard

        [DllImport(USER32, EntryPoint = "keybd_event")]
        public static extern void Keybd_Event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        #endregion

        #region  Process / Memory

        [DllImport(USER32)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint dwProcessId);

        [DllImport(KERNEL32)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport(KERNEL32)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport(KERNEL32)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        [DllImport(KERNEL32)]
        static extern bool CloseHandle(IntPtr handle);

        [DllImport(KERNEL32)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [DllImport(KERNEL32)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        #endregion
    }
}
