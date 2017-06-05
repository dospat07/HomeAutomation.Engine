using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class ChartsQuery : IChartsQuery
    {
        public object GetHourlyTemperatures(DateTime fromDate, DateTime toDate)
        {
            using (var ctx = new HomeAutomationContext())
            {
                var temperatures = ctx.GetHourlyTemperatures(fromDate, toDate);
                return GetData(temperatures, "MM-dd HH:mm");
            }

        }

        public object GetDailyTemperatures(DateTime fromDate, DateTime toDate)
        {
            using (var ctx = new HomeAutomationContext())
            {
                var temperatures = ctx.GetDailyTemperatures(fromDate, toDate);               
                return GetData(temperatures, "MM-dd");
            }
        }

        public object GetData(List<Temperature> temperatures, string format)
        {
            using (var ctx = new HomeAutomationContext())
            {
                var q = from r in ctx.Rooms
                        select new
                        {
                            label = r.Name,
                            data = (from t in temperatures where t.RoomID == r.ID orderby t.Date select t.Value).ToList()

                        };
                var result = new
                {
                    labels = temperatures.OrderBy(d => d.Date).Select(d => d.Date.ToString(format)).Distinct().ToList(),
                    datasets = q.ToList()
                };
                return result;
            }
        }
 
    }
}
