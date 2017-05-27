
using System.Collections.Generic;
namespace HomeAutomation.Engine.Models
{
    public interface IUserQuery
    {
        IEnumerable<User> GetAll();
    }
}