using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HomeAutomation.Engine.Persistance
{
    public class RoomDbRepository : IRoomRepository,IRoomQuery
    {
        public Room Add(string nodeAddress, string name, AirCondition airCondition)
        {
            using (var context = new HomeAutomationContext())
            {
                int id = context.Rooms.Max(o => o.ID) + 1;
                var room = new Room() { ID = id, NodeAddress = nodeAddress, Name = name, AirCondition = airCondition };
                context.Rooms.Add(room);
                context.SaveChanges();
                return room;
            }
        }

        public void Delete(int ID)
        {
            using (var context = new HomeAutomationContext())
            {
                context.Remove(context.Rooms.Where(o => o.ID == ID).FirstOrDefault());
                context.SaveChanges();
            }
        }

        public Room Get(int ID)
        {
            using (var context = new HomeAutomationContext())
            {
                return context.Rooms.Where(o=>o.ID==ID).FirstOrDefault();
            }
        }

        public IEnumerable<Room> GetAll()
        {
            using (var context = new HomeAutomationContext())
            {
                return context.Rooms.ToList();
            }
        }

        public void Update(Room room)
        {
            using (var context = new HomeAutomationContext())
            {
                var item = context.Rooms.Where(o => o.ID == room.ID).FirstOrDefault();

                item.AirCondition = room.AirCondition;
                item.Name = room.Name;
                item.NodeAddress = room.NodeAddress;
                context.SaveChanges();
            }
        }

        public void UpdateTemperature(int ID, float? temperature)
        {
            using (var context = new HomeAutomationContext())
            {
                var item = context.Rooms.Where(o => o.ID == ID).FirstOrDefault();

                item.Temperature = temperature;
                context.SaveChanges();
            }
        }
    }
}
