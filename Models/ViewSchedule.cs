using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Persistance;

namespace HomeAutomation.Engine.Models
{
    public class ViewSchedule
    {
        public int ID { get; set; }
        public string Time { get; set; }
        public string Room { get; set; }
        public string Days { get; set; }
        public short Mode { get; set; }
        public short Fan { get; set; }
        public short Temperature { get; set; }
       
    }
}
