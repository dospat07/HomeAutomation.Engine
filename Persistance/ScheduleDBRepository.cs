using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Services;

namespace HomeAutomation.Engine.Persistance
{
    public class ScheduleDbRepository : IScheduleRepository, IScheduleQuery
    {
        private IViewScheduleFactory scheduleFactory;

        public ScheduleDbRepository(IViewScheduleFactory scheduleFactory)
        {
            this.scheduleFactory = scheduleFactory;
        }

        public Schedule Add(bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun, int roomID, DateTime time, short temperture, short fan, Mode mode)
        {
            int id = 0;

            using (var context = new HomeAutomationContext())
            {
                if (context.Schedules.Count() > 0)

                {
                    id = context.Schedules.Max(o => o.ID) + 1;
                }

                var schedule = new Schedule()
                {
                    ID = id,
                    Mon = mon,
                    Tue = tue,
                    Wed = wed,
                    Thu = thu,
                    Fri = fri,
                    Sun = sun,
                    Sat = sat,
                    RoomID = roomID,
                    Time = time,
                    Mode = mode,
                    Fan = fan,
                    Temperature = temperture

                };

                context.Schedules.Add(schedule);
                context.SaveChanges();
                return schedule;
            }


        }

        public void Delete(int iD)
        {
            using (var context = new HomeAutomationContext())
            {
                var r = context.Schedules.Where(o => o.ID == iD).FirstOrDefault();
                context.Schedules.Remove(r);
                context.SaveChanges();
            }
        }

        public IEnumerable<Schedule> GetAll()
        {
            using (var context = new HomeAutomationContext())
            {
                return context.Schedules.ToList();
            }
        }

        public IEnumerable<dynamic> GetFormated()
        {
            using (var context = new HomeAutomationContext())
            {


                List<dynamic> result = new List<dynamic>();
                foreach (var schedule in context.Schedules)
                {


                    result.Add(this.scheduleFactory.Create(schedule));
                }
                return result;
            }
        }
    }
}
