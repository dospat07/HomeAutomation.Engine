using System;

namespace HomeAutomation.Engine.CQRS
{
    public interface IContainerResolver
    {
        object GetService(Type type);
    }
}