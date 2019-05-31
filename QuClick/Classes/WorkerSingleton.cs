using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuClick.Classes
{
    class WorkerSingleton
    {
        private static WorkerSingleton uniqueInstance = null;
        private SettingsSingleton settings = SettingsSingleton.GetInstance();
        public int Frequency { get; set; }

        private WorkerSingleton()
        {
            Frequency = 1;
        }

        public static WorkerSingleton GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new WorkerSingleton();
            }

            return uniqueInstance;
        }

        public void OnHotKeyPressed()
        {
            while (true)
            {
                if (settings.mouseFixed == true)
                {
                    CursorPositionHandler.SetCursorPos(CursorPositionHandler.fixedPosition.X, CursorPositionHandler.fixedPosition.Y);
                }

                MouseClicker mouseClicker = new MouseClicker();
                mouseClicker.ClickMouse();
                Thread.Sleep(1000 / Frequency);
            }
        }
    }
}
