using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models.Charts
{
    public class Dataset
    {
        [JsonProperty(PropertyName = "label")]
        string Label { get; set; }
        [JsonProperty(PropertyName = "data")]
        List<float> Data { get; set; }
    }
}
