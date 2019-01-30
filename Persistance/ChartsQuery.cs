using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class ChartsQuery : IChartsQuery
    {
        public ChartData GetHourlyTemperatures(DateTime fromDate, DateTime toDate)
        {
            using (var ctx = new HomeAutomationContext())
            {
                var temperatures = ctx.GetHourlyTemperatures(fromDate, toDate);
                return GetData(temperatures, "MM-dd HH:mm");
            }

        }

        public ChartData GetDailyTemperatures(DateTime fromDate, DateTime toDate)
        {
            using (var ctx = new HomeAutomationContext())
            {
                var temperatures = ctx.GetDailyTemperatures(fromDate, toDate);               
                return GetData(temperatures, "MM-dd");
            }
        }

        private ChartData GetData(List<Temperature> temperatures, string format)
        {
            using (var ctx = new HomeAutomationContext())
            {
                var q = from r in ctx.Devices
                        select new Dataset()
                        {
                            Label = r.Name,
                            Data = (from t in temperatures where t.DeviceID == r.ID orderby t.Date select t.Value).ToList()

                        };
                var result = new ChartData()
                {
                    Labels = temperatures.OrderBy(d => d.Date).Select(d => d.Date.ToString(format)).Distinct().ToList(),
                    Datasets = q.ToList()
                };
                return result;
            }
        }
 
    }
}
