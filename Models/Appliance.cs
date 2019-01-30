using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Appliance :short
    {
      
        Daikin = 0,
        Toshiba
    }
}
