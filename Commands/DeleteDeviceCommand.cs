using HomeAutomation.Engine.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Commands
{
    public class DeleteDeviceCommand:ICommand
    {
        public int ID { get; set; }
    }
}
