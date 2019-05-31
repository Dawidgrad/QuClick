using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuClick.Classes
{
    class SettingsSingleton
    {
        private static SettingsSingleton uniqueInstance = null;
        public Key? startStopKeybind { get; set; }
        public Key? toggleKeybind { get; set; }
        public int clicksPerSecond { get; set; }
        public double mouseX { get; set; }
        public double mouseY { get; set; }
        public bool mouseFixed { get; set; }


        private SettingsSingleton()
        {
            clicksPerSecond = 20;
            mouseFixed = false;
        }

        public static SettingsSingleton GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new SettingsSingleton();
            }

            return uniqueInstance;
        }
    }
}
