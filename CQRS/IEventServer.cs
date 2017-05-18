namespace HomeAutomation.Engine.CQRS
{
    public interface IEventServer
    {
        void SendToGroup(EventTypes type, string group, object @event);
        void SendToUser(EventTypes type, string user, object @event);
        void SendToAll(EventTypes type, object @event);
       // void SendToConnection(EventTypes type, string connectionID, object @event);)
    }
}