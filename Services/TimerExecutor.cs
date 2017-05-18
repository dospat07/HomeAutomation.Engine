using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeAutomation.Engine.Commands;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Services
{
    public class TimerExecutor:ITimerExecutor
    {
        private readonly Timer timer;

        public int? Period { get; private set; }

        public bool Started { get; private set; }

        

        public TimerExecutor(object state)
        {
            timer = new Timer(TimerCallback, state, Timeout.Infinite, Timeout.Infinite);
        }
      

        public void Start(int period)
        {
            this.timer.Change(0, period);
            Period = period;
            Started = true;
        }

        public void Stop()
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
        }


        protected  virtual void TimerCallback(object state)
        {
            
        }
    }
}
