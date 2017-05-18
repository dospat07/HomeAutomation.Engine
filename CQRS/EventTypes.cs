using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.CQRS
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventTypes
    {
        RoomCreated,
        RoomDeleted,
        RoomUpdated,
        TemperatureUpdated,
        Error,
        ScheduleCreated,
        ScheduleDeleted,
        CommandSend
        
    }
}
