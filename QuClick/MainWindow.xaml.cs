using QuClick.Classes;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;


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

        private HwndSource _source;
        private const int HOTKEY_ID = 9000;
        private Thread workerThread = null;
        WorkerSingleton worker = WorkerSingleton.GetInstance();

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

                // Give user 2 seconds to enter the keybind
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

                // Give user 2 seconds to enter the keybind
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

            // Register the key that was pressed by the user
            uint keyId = (uint)KeyInterop.VirtualKeyFromKey(e.Key);
            toggleHandler = new KeyHandler(keyId, HOTKEY_ID, this);
            toggleHandler.RegisterHotKey();
        }

        // Save Start / Stop keybind when key press is detected
        private void StartStop_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            settings.startStopKeybind = e.Key;
            StartStopLabel.Text = e.Key.ToString();

            // Register the key that was pressed by the user
            uint keyId = (uint)KeyInterop.VirtualKeyFromKey(e.Key);
            startStopHandler = new KeyHandler(keyId, HOTKEY_ID, this);
            startStopHandler.RegisterHotKey();
        }

        private void FixKeybind_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FrequencyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.worker.Frequency = Int32.Parse(Frequency.Text);
            }
            catch (Exception ex)
            {
                // Error handling
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            // Initialise global hooks
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
        }

        protected override void OnClosed(EventArgs e)
        {
            // Remove global hooks before closing the window
            _source.RemoveHook(HwndHook);
            _source = null;
            
            // Unregister keybinds
            if (toggleHandler != null)
                toggleHandler.UnregisterHotKey();

            if (startStopHandler != null)
                startStopHandler.UnregisterHotKey();
            
            base.OnClosed(e);
        }


        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            // Check for current message passed to the program
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            // Decide wether to start the clicker or to stop it
                            if (workerThread == null)
                            {
                                workerThread = new Thread(new ThreadStart(worker.OnHotKeyPressed));
                                workerThread.Start();
                            }
                            else
                            {
                                workerThread.Abort();
                                workerThread = null;
                            }

                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }
    }


}
