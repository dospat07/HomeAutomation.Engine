using HomeAutomation.Engine.CQRS;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Services
{
    public class EventServer : IEventServer
    {
        IHubContext<EngineHub> hub;
        public EventServer(IHubContext<EngineHub>  hub)
        {
            this.hub =hub;
        }
        public void SendToAll(EventTypes type, object @event)
        {
            Log(type, @event);
        }

        public void SendToGroup(EventTypes type, string group, object @event)
        {
            Log(type, @event);
        }

        public void SendToUser(EventTypes type, string user, object @event)
        {
            Log(type, @event);  
        }

        private void Log(EventTypes type, object @event)
        {

            var msg = new { type = $"{type}", message = @event };
            hub.Clients.All.SendAsync("onEvent",msg );

           
            //hub.Clients.All.onEvent(type,@event);
            Console.WriteLine($"Event {type} data {@event}");
        }
    }
}
