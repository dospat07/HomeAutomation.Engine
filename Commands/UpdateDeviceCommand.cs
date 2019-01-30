using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Commands
{
    public class UpdateDeviceCommand:ICommand
    {
        public int ID { get; set;}
        public string NodeAddress { get; set; }
        public string Name { get; set; }
        public Appliance Appliance { get; set; }
    }
}
