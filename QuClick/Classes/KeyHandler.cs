using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;

namespace QuClick.Classes
{
    public class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int id;

        public KeyHandler(int key)
        {
            this.key = (int)key;
            this.hWnd = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            id = this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, 9000, 0, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }


    }
}
