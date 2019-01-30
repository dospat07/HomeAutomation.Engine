using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HomeAutomation.Engine.Persistance
{
    public class DeviceDbRepository : IDeviceRepository,IDeviceQuery
    {
        public Device Add(string nodeAddress, string name, Appliance  appliance)
        {
            using (var context = new HomeAutomationContext())
            {
                int id = context.Devices.Max(o => o.ID) + 1;
                var device = new Device() { ID = id, NodeAddress = nodeAddress, Name = name, Appliance = appliance };
                context.Devices.Add(device);
                context.SaveChanges();
                return device;
            }
        }

        public void Delete(int ID)
        {
            using (var context = new HomeAutomationContext())
            {
                context.Remove(context.Devices.Where(o => o.ID == ID).FirstOrDefault());
                context.SaveChanges();
            }
        }

        public Device Get(int ID)
        {
            using (var context = new HomeAutomationContext())
            {
                return context.Devices.Where(o=>o.ID==ID).FirstOrDefault();
            }
        }

        public IEnumerable<Device> GetAll()
        {
            using (var context = new HomeAutomationContext())
            {
                return context.Devices.ToList();
            }
        }

        public void Update(Device device)
        {
            using (var context = new HomeAutomationContext())
            {
                var item = context.Devices.Where(o => o.ID == device.ID).FirstOrDefault();

                item.Appliance = device.Appliance;
                item.Name = device.Name;
                item.NodeAddress = device.NodeAddress;
                context.SaveChanges();
            }
        }

        public void UpdateTemperature(int ID, float? temperature)
        {
            using (var context = new HomeAutomationContext())
            {
                var item = context.Devices.Where(o => o.ID == ID).FirstOrDefault();

                item.Temperature = temperature;
                context.SaveChanges();
            }
        }
    }
}
