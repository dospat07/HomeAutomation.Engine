using HomeAutomation.Engine.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public interface IChartsQuery
    {
        ChartData GetHourlyTemperatures(DateTime fromDate, DateTime toDate);
        ChartData GetDailyTemperatures(DateTime fromDate, DateTime toDate);
    }
}
