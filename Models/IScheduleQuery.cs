using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public interface IScheduleQuery
    {
        IEnumerable<Schedule> GetAll();
        IEnumerable<dynamic> GetFormated();
    }
}
