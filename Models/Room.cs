using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    //public class Test
    //{
    //    public string Prop1 { get; set; } = "Prop1";
    //    public string Prop2 { get; set; } = "Prop2";
       
    //}
    public class Room
    {
        public int ID { get; set; }
        public string NodeAddress { get; set; }
        public string Name { get; set; }
        public AirCondition AirCondition { get; set; }
        public virtual float? Temperature { get; set; } = null;
     
    }
}
