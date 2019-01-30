using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Commands;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Services;

namespace HomeAutomation.Engine.CommandHandler
{
    public class ScheduleHandlers  : ICommandHandler<AddScheduleCommand>, ICommandHandler<DeleteScheduleCommand>
    {

        private IScheduleRepository repository;
        private IEventServer eventServer;
        private IScheduleQuery scheduleQuery;
        private IViewScheduleFactory scheduleFactory;

        public ScheduleHandlers(IScheduleQuery scheduleQuery, IEventServer eventServer,
            IScheduleRepository scheduleRepository,IViewScheduleFactory scheduleFactory)
        {
            this.scheduleQuery = scheduleQuery;
            this.eventServer = eventServer;
            this.repository = scheduleRepository;
            this.scheduleFactory = scheduleFactory;
        }

        public void Handle(AddScheduleCommand command)
        {
            if (command.Mon || command.Tue || command.Wed || command.Thu || command.Fri || command.Sat || command.Sun)
            {
                Schedule schedule = this.repository.Add(command.Mon, command.Tue, command.Wed, command.Thu, command.Fri,
                    command.Sat, command.Sun, command.DeviceID, command.Time, command.Temperature, command.Fan,
                    command.Mode);
                eventServer.SendToAll(EventTypes.ScheduleCreated, this.scheduleFactory.Create(schedule));
            }
            else
            {

                //Bad Request
            }

        }

        public void Handle(DeleteScheduleCommand command)
        { 
            repository.Delete(command.ID);
            eventServer.SendToAll(EventTypes.ScheduleDeleted,command.ID);
        }
    }
}
