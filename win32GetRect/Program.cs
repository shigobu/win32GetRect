using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace win32GetRect
{
	class Program
	{
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

		[DllImport("user32.dll", SetLastError = true)]
		static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void SetCursorPos(int X, int Y);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const uint WM_LBUTTONDOWN = 0x0201;
		private const uint WM_LBUTTONUP = 0x0202;
        private const uint WM_MOUSEMOVE = 0x0200;

        private const uint MK_LBUTTON = 0x0001;
        private const uint MK_CONTROL = 0x0008;

        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;

        static void Main(string[] args)
		{
            string processName = "dspMixFx_UR28M";
            Process[] pros = Process.GetProcessesByName(processName);
			Process pro = null;
			foreach (var item in pros)
			{
				pro = item;
				break;
			}
			if (pro == null)
			{
				return;
			}

			IntPtr handle = pro.MainWindowHandle;
			RECT rect;
			GetWindowRect(handle, out rect);
			Console.WriteLine("ウィンドウサイズ");
			Console.WriteLine("X {0}", rect.Right - rect.Left);
			Console.WriteLine("Y {0}", rect.Bottom - rect.Top);

			RECT rect2;
			GetClientRect(handle, out rect2);
			Console.WriteLine("クライアントウサイズ");
			Console.WriteLine("X {0}", rect2.Right - rect2.Left);
			Console.WriteLine("Y {0}", rect2.Bottom - rect2.Top);

            Microsoft.VisualBasic.Interaction.AppActivate(pro.Id);
            System.Threading.Thread.Sleep(100);

            //SendMessage(handle, WM_LBUTTONDOWN, MK_LBUTTON, MAKELPARAM(35, 200));
            //System.Threading.Thread.Sleep(50);
            //SendMessage(handle, WM_MOUSEMOVE, MK_LBUTTON, MAKELPARAM(35, 200));
            //System.Threading.Thread.Sleep(50);
            //SendMessage(handle, WM_LBUTTONUP, 0, MAKELPARAM(35, 200));

            //SetCursorPos(rect.Left + 37, rect.Top + 200);
            SetCursorPos(rect.Left + 24, rect.Top + 210);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);              // マウスの左ボタンダウンイベントを発生させる
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);                // マウスの左ボタンアップイベントを発生させる

            Console.Read();
		}

		private static uint MAKELPARAM(short x, short y)
		{
			int temp = x * 0x10000;
			uint ret = (uint)temp + (ushort)y;
			return ret;
		}
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int Left;        // x position of upper-left corner
		public int Top;         // y position of upper-left corner
		public int Right;       // x position of lower-right corner
		public int Bottom;      // y position of lower-right corner
	}
}
