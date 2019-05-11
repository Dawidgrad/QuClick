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
        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey(
            [In] IntPtr hWnd,
            [In] int id,
            [In] uint fsModifiers,
            [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey(
            [In] IntPtr hWnd,
            [In] int id);

        private uint key;
        private int hotkeyId;
        Window window;

        public KeyHandler(uint key, int hotkeyId, Window window)
        {
            this.key = key;
            this.hotkeyId = hotkeyId;
            this.window = window;
        }
        
        public void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(window);
            if (!RegisterHotKey(helper.Handle, hotkeyId, 0, key))
            {
                // handle error
            }
        }

        public void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(window);
            UnregisterHotKey(helper.Handle, hotkeyId);
        }


    }
}
