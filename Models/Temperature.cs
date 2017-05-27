using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public class Temperature
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }
    }

    public class HourlyTemperature
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }
    }
}
