﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.CQRS
{
    public interface ICommandBus
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
