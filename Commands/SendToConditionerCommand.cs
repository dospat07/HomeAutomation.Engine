﻿using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Commands
{
    public class SendToConditionerCommand:ICommand
    {
        public ApplianceCommand Command { get; set; }
        public int DeviceID { get; set; }
    }
}
