using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Models
{
    public interface IRoomRepository
    {
        Room Add(string nodeAddress, string name, AirCondition airCondition);
        void Delete(int iD);
        void Update(Room room);
        void UpdateTemperature(int ID, float? temperature);
    }
}