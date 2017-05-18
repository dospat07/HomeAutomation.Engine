using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Services
{
    public interface IViewScheduleFactory
    {
        ViewSchedule Create(Schedule schedule);
    }
}
