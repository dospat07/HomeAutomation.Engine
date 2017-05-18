using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.CQRS
{
    public class CommandBus : ICommandBus
    {
        private readonly IContainerResolver resolver;

        public CommandBus(IContainerResolver resolver)
        {
            this.resolver = resolver;
        }

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)resolver.GetService(typeof(ICommandHandler<TCommand>));
            if(handler== null)
            {
                Console.WriteLine($"Handler for {typeof(ICommandHandler<TCommand>)} is null");
            }
            handler?.Handle(command);
        }
    }
}
