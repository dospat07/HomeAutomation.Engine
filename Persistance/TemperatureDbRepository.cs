using HomeAutomation.Engine.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public class TemperatureDbRepository : ITemperatureRepository
    { 

        Temperature ITemperatureRepository.Add(int roomID, float temperature, DateTime date)
        {
            using (var context = new HomeAutomationContext())
            {
                int id = context.Temperatures.Max(o => o.ID) + 1;
                var temp = new Temperature() { ID = id, RoomID =roomID,Value = temperature,Date=date};
                context.Temperatures.Add(temp);
                context.SaveChanges();
                return temp;
            }
        }
    }
}
