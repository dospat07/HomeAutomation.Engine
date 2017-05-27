using HomeAutomation.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Persistance
{
    public class UserDbRepository : IUserQuery
    {
        public IEnumerable<User> GetAll()
        {
            using (var context = new HomeAutomationContext())
            {
                return context.Users.ToList();
            }
        }
    }
}
