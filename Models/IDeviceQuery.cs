using System.Collections.Generic;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Models
{
    public interface IDeviceQuery
    {
        IEnumerable<Device> GetAll();
        Device Get(int ID);
    }
}