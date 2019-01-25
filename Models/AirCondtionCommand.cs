using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public class AirCondtionCommand
    {
        
        public short Temperature { get; set; }
        public short Fan { get; set; }  
        public Mode Mode { get; set; }
 
        public override string ToString()
        {
            return $"Temp {Temperature} Fan {Fan} Mode {Mode}";
        }
    }
}
