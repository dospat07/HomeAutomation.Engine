using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Commands;

namespace HomeAutomation.Engine.Services
{
    class State
    {
        public ICommandBus CommandBus { get; set; }
        public IScheduleQuery ScheduleQuery { get; set; }
    }
    public class ScheduleCommandExecutor : TimerExecutor, IScheduleCommandExecutor
    {
        public ScheduleCommandExecutor(IScheduleQuery query, ICommandBus commandBus)
            : base(new State() { CommandBus = commandBus, ScheduleQuery = query })
        {
        }

        protected override void TimerCallback(object state)
        {
            var _state = (State)state;
            SendToConditionerCommand command = new SendToConditionerCommand();
            DateTime now = DateTime.Now;

            foreach (var schedule in _state.ScheduleQuery.GetAll().Where(
                o => (
                (o.Time.Hour == now.Hour) &&
                (o.Time.Minute == now.Minute) &&
                ((now.DayOfWeek == DayOfWeek.Friday && o.Fri) |
                (now.DayOfWeek == DayOfWeek.Saturday && o.Sat) |
                (now.DayOfWeek == DayOfWeek.Sunday && o.Sun) |
                (now.DayOfWeek == DayOfWeek.Monday && o.Mon) |
                (now.DayOfWeek == DayOfWeek.Tuesday && o.Tue) |
                (now.DayOfWeek == DayOfWeek.Wednesday && o.Wed) |
                (now.DayOfWeek == DayOfWeek.Thursday && o.Thu)))
                      ).ToList())
            {
                Console.WriteLine(command);
                command.RoomID = schedule.RoomID;
                command.Command = new AirCondtionCommand()
                {
                    Mode =  schedule.Mode,
                    Fan = schedule.Fan,
                    Temperature = schedule.Temperature
                };

                _state.CommandBus.Execute(command);

            }
        }
    }
}

