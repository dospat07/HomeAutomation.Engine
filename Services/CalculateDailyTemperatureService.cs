using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Services
{
    public class CalculateDailyTemperatureService : TimerExecutor,ICalculateDailyTemperatureService
    {
        public CalculateDailyTemperatureService() : base(null)
        {
            Console.WriteLine("CalculateDailyTemperatureService created");
        }

        protected override void TimerCallback(object state)
        {
            Console.WriteLine("CalculateDailyTemperatureService timer");
            using (var context = new HomeAutomation.Engine.Persistance.HomeAutomationContext())
            {
                context.CalculateDailyTemperature();
            }
        }
    }
}
