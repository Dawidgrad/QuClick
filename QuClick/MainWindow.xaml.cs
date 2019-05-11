using QuClick.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool canClick = true;
        private SettingsSingleton settings;
        private KeyHandler startStopHandler;
        private KeyHandler toggleHandler;


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

        private HwndSource _source;
        private const int HOTKEY_ID = 9000;

        public MainWindow()
        {
            InitializeComponent();
            settings = SettingsSingleton.GetInstance();
        }

        // Detect when the Start / Stop button was clicked
        private async void StartStopKeybind_Click(object sender, RoutedEventArgs e)
        {
            if (canClick)
            {
                this.canClick = false;

                PreviewKeyDown += StartStop_PreviewKeyDown;
                await Task.Delay(2000);
                PreviewKeyDown -= StartStop_PreviewKeyDown;

                this.canClick = true;
            }
        }

        // Detect when the toggle button was clicked
        private async void ToggleKeybind_Click(object sender, RoutedEventArgs e)
        {
            if (canClick)
            {
                this.canClick = false;

                PreviewKeyDown += Toggle_PreviewKeyDown;
                await Task.Delay(2000);
                PreviewKeyDown -= Toggle_PreviewKeyDown;

                this.canClick = true;
            }
        }

        // Save Toggle keybind when key press is detected
        private void Toggle_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            settings.toggleKeybind = e.Key;
            ToggleLabel.Text = e.Key.ToString();

            //toggleHandler = new KeyHandler(settings.toggleKeybind);
            //toggleHandler.Register();
        }

        // Save Start / Stop keybind when key press is detected
        private void StartStop_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            settings.startStopKeybind = e.Key;
            StartStopLabel.Text = e.Key.ToString();

            //startStopHandler = new KeyHandler(settings.startStopKeybind);
            //bool result = startStopHandler.Register();
        }

        private void FixKeybind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FrequencyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            RegisterHotKey();
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
            base.OnClosed(e);
        }

        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            const uint VK_F10 = 0x79;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, 0, VK_F10))
            {
                // handle error
            }
        }

        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            MouseClicker mouseClicker = new MouseClicker();
            mouseClicker.ClickMouse();
        }
    }


}
