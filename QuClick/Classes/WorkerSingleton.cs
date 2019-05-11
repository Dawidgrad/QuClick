using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuClick.Classes
{
    class WorkerSingleton
    {
        private static WorkerSingleton uniqueInstance = null;

        private WorkerSingleton()
        {
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
                MouseClicker mouseClicker = new MouseClicker();
                mouseClicker.ClickMouse();
                Thread.Sleep(1000);
            }
        }
    }
}
