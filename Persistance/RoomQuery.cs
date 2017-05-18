using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class RoomQuery : IRoomQuery
    {
        public Room Get(int ID)
        {
           return  RoomRepository.Rooms.Where(o => o.ID == ID).FirstOrDefault();
        }

        public IEnumerable<Room> GetAll()
        {
            return RoomRepository.Rooms;
        }
    }
}
