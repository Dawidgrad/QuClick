using QuClick.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        SettingsSingleton settings;

        public MainWindow()
        {
            InitializeComponent();
            settings = SettingsSingleton.GetInstance();
            KeyDown += GetKey_KeyDownEvent;
        }

        private void GetKey_KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (settings.startStopKeybind == e.Key)
            {
                MouseClicker mouseClicker = new MouseClicker();
                mouseClicker.ClickMouse();
            }
            else if (settings.toggleKeybind == e.Key)
            {

            }
        }

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

        private void Toggle_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            settings.toggleKeybind = e.Key;
            ToggleLabel.Text = e.Key.ToString();
        }
        private void StartStop_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            settings.startStopKeybind = e.Key;
            StartStopLabel.Text = e.Key.ToString();
        }

        private void FixKeybind_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
