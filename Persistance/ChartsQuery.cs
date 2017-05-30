using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class ChartsQuery : IChartsQuery
    {
        public  object  GetTemperatures(DateTime fromDate, DateTime toDate)
        {


            using (var ctx = new HomeAutomationContext())
            {

                var q = from r in ctx.Rooms
                        select new
                        {
                            label = r.Name,
                            //data = ctx.HourlyTemperatures.Where(p => p.RoomID == r.ID && p.Date>fromDate &&p.Date<toDate).OrderBy(d => d.Date).Select(v => v.Value).ToList()
                            data = (from t in ctx.HourlyTemperatures where t.RoomID == r.ID && t.Date > fromDate && t.Date < toDate orderby t.Date select t.Value).ToList()

                        };
                var result = new
                {
                    labels = ctx.HourlyTemperatures.Where(p => p.Date > fromDate && p.Date < toDate).OrderBy(d => d.Date).Select(d => d.Date.ToString("MM-dd HH:mm")).Distinct().ToList(),
                    datasets = q.ToList()
                };
                return result;
            }
            
        }
    }
}
