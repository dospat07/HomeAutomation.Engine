using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models.Charts
{
    public class ChartData
    {
        [JsonProperty(PropertyName = "labels")]
        public List<string> Labels { get; set; }
        [JsonProperty(PropertyName = "datasets")]
        public List<Dataset> Datasets { get; set; }
    }
}
