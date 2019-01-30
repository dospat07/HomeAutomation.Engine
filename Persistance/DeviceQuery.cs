using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class DeviceQuery : IDeviceQuery
    {
        public Device Get(int ID)
        {
           return  DeviceRepository.Devices.Where(o => o.ID == ID).FirstOrDefault();
        }

        public IEnumerable<Device> GetAll()
        {
            return DeviceRepository.Devices;
        }
    }
}
