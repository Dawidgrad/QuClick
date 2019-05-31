using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuClick.Classes
{
    class CursorPositionHandler
    {
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [DllImport("User32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static POINT fixedPosition;

        public static void SaveCursorPosition()
        {
            GetCursorPos(out fixedPosition);
        }
    }
}
