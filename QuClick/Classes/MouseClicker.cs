using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuClick.Classes
{
    class MouseClicker
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        

        public static Point GetCursorPosition()
        {
            Point lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

        // Simulate mouse click
        public void ClickMouse()
        {
            Point mousePoint = GetCursorPosition();
            uint X = (uint)mousePoint.X;
            uint Y = (uint)mousePoint.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }


    }
}
