using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public interface IChartsQuery
    {
        object GetHourlyTemperatures(DateTime fromDate, DateTime toDate);
        object GetDailyTemperatures(DateTime fromDate, DateTime toDate);
    }
}
