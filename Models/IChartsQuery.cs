using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public interface IChartsQuery
    {
        object GetTemperatures(DateTime from, DateTime to);
        
    }
}
