using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public interface ITemperatureRepository
    {
        Temperature Add(int deviceID, float temperature, DateTime date);
        
       
    }
}
