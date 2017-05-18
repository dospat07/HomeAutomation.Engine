using HomeAutomation.Engine.Commands;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Services
{
    public class TemperatureReader :TimerExecutor, ITemperatureReader
    {
        class State
        {
            public ICommandBus CommandBus { get; set; }
            public IRoomQuery RoomQuery { get; set; }
        }
       
        public TemperatureReader(IRoomQuery query, ICommandBus commandBus):base(new State() { CommandBus = commandBus, RoomQuery = query })
        {
             
        }

        protected  override void TimerCallback(object state)
        {
            var  _state = (State) state;
            ReadTemperatureCommand command = new ReadTemperatureCommand();
            foreach (var room in _state.RoomQuery.GetAll())
            {
                command.RoomID = room.ID;
                 
                _state.CommandBus.Execute(command);
                // Console.WriteLine($"Node {node.ID} Temp {node.Temperature}");
            }
        }


    }
}
