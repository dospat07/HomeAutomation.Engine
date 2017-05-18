using System;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Commands
{
    public class AddScheduleCommand : ICommand
    {

        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
        public DateTime Time { get; set; }
        public int RoomID { get; set; }
        public short Temperature { get; set; }
        public short Fan { get; set; }
        public Mode Mode { get; set; }
    }
}