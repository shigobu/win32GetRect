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

			GetClientRect(handle, out rect);
			Console.WriteLine("クライアントウサイズ");
			Console.WriteLine("X {0}", rect.Right - rect.Left);
			Console.WriteLine("Y {0}", rect.Bottom - rect.Top);

			Console.Read();
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
