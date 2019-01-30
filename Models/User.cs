using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public class User
    {
        public int ID { get; internal set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
