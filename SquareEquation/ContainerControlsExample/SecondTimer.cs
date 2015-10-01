using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ContainerControlsExample
{
    /// <summary>
    /// Возвращает текущее значение таймера
    /// </summary>
    public class SecondTimer
    {
        public DateTime BeginValue { get; set; }

        public SecondTimer()
        {
            RefreshTimer();
        }

        public TimeSpan GetNextValue()
        {
            return DateTime.Now - BeginValue;
        }

        public void RefreshTimer()
        {
            BeginValue = DateTime.Now;
        }

    }
}
