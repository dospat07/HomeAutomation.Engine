using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class DeviceRepository : IDeviceRepository
    {


        public static List<Device> Devices = new List<Device>()
        {
            new Device() { ID =1, NodeAddress ="http://132.83.61.8:5001/api",Name = "Hall", Appliance = Appliance .Daikin },
            new Device() { ID =2, NodeAddress ="http://132.83.61.8:5001/api",Name = "Kitchen", Appliance = Appliance .Toshiba }
        };

        static object sync = new object();
        public Device Add(string nodeAddress, string name, Appliance  airCondition)
        {
            int id;
            lock (sync)
            {
                id = Devices.Max(o => o.ID) + 1;
            }
            var device = new Device() { ID = id, NodeAddress = nodeAddress, Name = name, Appliance = airCondition };
            lock (sync)
            {
                Devices.Add(device);
            }
          
            return device;
        }

        public void Delete(int ID)
        {
            lock (sync)
            {
                Devices.Remove(Devices.Where(o => o.ID == ID).FirstOrDefault());
            }
        }

        public void Update(Device device)
        {
            lock (sync)
            {
                var item= Devices.Where(o => o.ID == device.ID).FirstOrDefault();

                item.Appliance = device.Appliance;
                item.Name = device.Name;
                item.NodeAddress = device.NodeAddress;
                
            }
        }

        public void UpdateTemperature(int ID, float? temperature)
        {
            lock (sync)
            {
                var item = Devices.Where(o => o.ID == ID).FirstOrDefault();
                item.Temperature = temperature;

            }
        }
    }
}
