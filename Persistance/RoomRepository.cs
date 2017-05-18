using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class RoomRepository : IRoomRepository
    {


        public static List<Room> Rooms = new List<Room>()
        {
            new Room() { ID =1, NodeAddress ="http://132.83.61.8:5001/api",Name = "Hall", AirCondition = AirCondition.Daikin },
            new Room() { ID =2, NodeAddress ="http://132.83.61.8:5001/api",Name = "Kitchen", AirCondition = AirCondition.Toshiba }
        };

        static object sync = new object();
        public Room Add(string nodeAddress, string name, AirCondition airCondition)
        {
            int id;
            lock (sync)
            {
                id = Rooms.Max(o => o.ID) + 1;
            }
            var room = new Room() { ID = id, NodeAddress = nodeAddress, Name = name, AirCondition = airCondition };
            lock (sync)
            {
                Rooms.Add(room);
            }
          
            return room;
        }

        public void Delete(int ID)
        {
            lock (sync)
            {
                Rooms.Remove(Rooms.Where(o => o.ID == ID).FirstOrDefault());
            }
        }

        public void Update(Room room)
        {
            lock (sync)
            {
                var item= Rooms.Where(o => o.ID == room.ID).FirstOrDefault();

                item.AirCondition = room.AirCondition;
                item.Name = room.Name;
                item.NodeAddress = room.NodeAddress;
                
            }
        }

        public void UpdateTemperature(int ID, float? temperature)
        {
            lock (sync)
            {
                var item = Rooms.Where(o => o.ID == ID).FirstOrDefault();
                item.Temperature = temperature;

            }
        }
    }
}
