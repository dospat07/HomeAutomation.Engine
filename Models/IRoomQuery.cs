using System.Collections.Generic;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Models
{
    public interface IRoomQuery
    {
        IEnumerable<Room> GetAll();
        Room Get(int ID);
    }
}