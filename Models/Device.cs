using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
   
    public class Device
    {
        public int ID { get; set; }
        public string NodeAddress { get; set; }
        public string Name { get; set; }
        public Appliance  Appliance { get; set; }
        public virtual float? Temperature { get; set; } = null;
     
    }
}
