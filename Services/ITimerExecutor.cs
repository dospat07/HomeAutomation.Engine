using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Services
{
    public interface ITimerExecutor
    {
        int? Period { get; }
        bool Started { get; }
        void Start(int period);
        void Stop();
    }
}
