using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.CQRS
{

    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}
