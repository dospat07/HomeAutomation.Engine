using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Models
{
    public interface IScheduleRepository
    {

        //public short Temperature { get; set; }
        //public short Fan { get; set; }
        //public Mode Mode { get; set; }
        Schedule Add(bool mon, bool tue, bool wed, bool thu,bool fri,bool sat,bool sun,int roomID,DateTime time,short temperture,short fan,Mode mode);
        void Delete(int iD);
    }
}
