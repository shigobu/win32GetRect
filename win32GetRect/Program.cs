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

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

		private const uint WM_LBUTTONDOWN = 0x0201;
		private const uint WM_LBUTTONUP = 0x0202;

		static void Main(string[] args)
		{
			Console.WriteLine("プロセス名の入力");
			string processName = Console.ReadLine();
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

			SendMessage(handle, WM_LBUTTONDOWN, 0, MAKELPARAM(-25, -34));
			System.Threading.Thread.Sleep(50);
			SendMessage(handle, WM_LBUTTONUP, 0, MAKELPARAM(25, 34));

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
