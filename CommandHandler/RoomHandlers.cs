using HomeAutomation.Engine.Commands;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.CommandHandler
{
    public class RoomHandlers : ICommandHandler<AddRoomCommand>, ICommandHandler<DeleteRoomCommand>, ICommandHandler<UpdateRoomCommand>
    {
        IRoomRepository repository;
        IEventServer eventServer;
        IRoomQuery roomQuery;
        public RoomHandlers(IRoomRepository repository, IEventServer eventServer,IRoomQuery roomQuery)
        {
            this.repository = repository;
            this.eventServer = eventServer;
            this.roomQuery = roomQuery;
        }
       

        public void Handle(UpdateRoomCommand command)
        {
            var room = new Room() { AirCondition = command.AirCondition, ID = command.ID, Name = command.Name, NodeAddress = command.NodeAddress };
            repository.Update(room);
            eventServer.SendToAll(EventTypes.RoomUpdated, room);
        }

        public void Handle(DeleteRoomCommand command)
        {
            repository.Delete(command.ID);
            eventServer.SendToAll(EventTypes.RoomDeleted, command.ID);
        }

        public void Handle(AddRoomCommand command)
        {
            Room room = repository.Add(command.NodeAddress, command.Name, command.AirCondition);
            eventServer.SendToAll(EventTypes.RoomCreated, room);
        }

       
    }
}
