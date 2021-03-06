﻿using QuClick.Classes;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace QuClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsSingleton settings;

        // Key handlers responsible for registering
        // and unregistering keys
        private KeyHandler startStopHandler;
        private KeyHandler toggleHandler;
        private KeyHandler fixMouseHandler;

        private HwndSource _source;
        private const int STARTSTOP_ID = 9000;
        private const int TOGGLE_ID = 9001;
        private const int FIXMOUSE_ID = 9002;

        // Thread to simulate mouse clicking in a loop
        private Thread workerThread = null;
        WorkerSingleton worker = WorkerSingleton.GetInstance();

        private bool canClick = true;

        public MainWindow()
        {
            InitializeComponent();
            settings = SettingsSingleton.GetInstance();
        }

        /**************************************************************************/
        #region Click events

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

        // Record the keybind to activate fixed cursor position
        private async void FixKeybind_Click(object sender, RoutedEventArgs e)
        {
            if (canClick)
            {
                this.canClick = false;

                // Give user 2 seconds to enter the keybind
                PreviewKeyDown += FixKeybind_PreviewKeyDown;
                await Task.Delay(2000);
                PreviewKeyDown -= FixKeybind_PreviewKeyDown;

                this.canClick = true;
            }
        }

        // Detect when the frequency button is clicked 
        private void FrequencyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.worker.Frequency = Int32.Parse(Frequency.Text);
                MessageBox.Show("The frequency was modified!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("The value passed was empty!", "Value error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("The value passed was in the wrong format!", "Value error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Value too high!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        /**************************************************************************/
        #region PreviewKeyDown events

        // Save Toggle keybind when key press is detected
        private void Toggle_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (settings.toggleKeybind != null)
                {
                    toggleHandler.UnregisterHotKey();
                }

                settings.toggleKeybind = e.Key;
                ToggleLabel.Text = e.Key.ToString();

                // Register the key that was pressed by the user
                uint keyId = (uint)KeyInterop.VirtualKeyFromKey(e.Key);
                toggleHandler = new KeyHandler(keyId, TOGGLE_ID, this);
                toggleHandler.RegisterHotKey();

                MessageBox.Show("The toggle keybind was modified!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The keybind could not be modified!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Save Start / Stop keybind when key press is detected
        private void StartStop_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (settings.startStopKeybind != null)
                {
                    startStopHandler.UnregisterHotKey();
                }
                
                settings.startStopKeybind = e.Key;
                StartStopLabel.Text = e.Key.ToString();

                // Register the key that was pressed by the user
                uint keyId = (uint)KeyInterop.VirtualKeyFromKey(e.Key);
                startStopHandler = new KeyHandler(keyId, STARTSTOP_ID, this);
                startStopHandler.RegisterHotKey();

                MessageBox.Show("The start / stop keybind was modified!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The keybind could not be modified!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void FixKeybind_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (settings.fixKeybind != null)
                {
                    fixMouseHandler.UnregisterHotKey();
                }

                settings.fixKeybind = e.Key;
                FixKeybindLabel.Text = e.Key.ToString();

                // Register the key that was pressed by the user
                uint keyId = (uint)KeyInterop.VirtualKeyFromKey(e.Key);
                fixMouseHandler = new KeyHandler(keyId, FIXMOUSE_ID, this);
                fixMouseHandler.RegisterHotKey();

                MessageBox.Show("Fix mouse position keybind was modified!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The keybind could not be modified!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        /**************************************************************************/
        #region Window events

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

        #endregion

        /**************************************************************************/
        #region Helper methods

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            // Check for current message passed to the program
            switch (msg)
            {
                case WM_HOTKEY:

                    switch (wParam.ToInt32())
                    {
                        case STARTSTOP_ID:
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
                        case FIXMOUSE_ID:
                            settings.mouseFixed = !settings.mouseFixed;
                            CursorPositionHandler.SaveCursorPosition();
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }
        
        private Point GetMousePos()
        {
            return this.PointToScreen(Mouse.GetPosition(this));
        }

        #endregion

    }


}
