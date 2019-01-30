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
    public class DeviceHandlers : ICommandHandler<AddDeviceCommand>, ICommandHandler<DeleteDeviceCommand>, ICommandHandler<UpdateDeviceCommand>
    {
        IDeviceRepository repository;
        IEventServer eventServer;
        IDeviceQuery deviceQuery;
        public DeviceHandlers(IDeviceRepository repository, IEventServer eventServer,IDeviceQuery deviceQuery)
        {
            this.repository = repository;
            this.eventServer = eventServer;
            this.deviceQuery = deviceQuery;
        }
       

        public void Handle(UpdateDeviceCommand command)
        {
            var device = new Device() { Appliance = command.Appliance, ID = command.ID, Name = command.Name, NodeAddress = command.NodeAddress };
            repository.Update(device);           
            eventServer.SendToAll(EventTypes.DeviceUpdated, device);
        }

        public void Handle(DeleteDeviceCommand command)
        {
            repository.Delete(command.ID);
            eventServer.SendToAll(EventTypes.DeviceDeleted, command.ID);
        }

        public void Handle(AddDeviceCommand command)
        {
            Device device = repository.Add(command.NodeAddress, command.Name, command.Appliance);
            eventServer.SendToAll(EventTypes.DeviceCreated, device);
        }

       
    }
}
