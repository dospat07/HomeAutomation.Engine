using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Models
{
    public interface IDeviceRepository
    {
        Device Add(string nodeAddress, string name, Appliance  appliance);
        void Delete(int iD);
        void Update(Device device);
        void UpdateTemperature(int ID, float? temperature);
    }
}