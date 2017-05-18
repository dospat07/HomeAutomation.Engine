using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;
using System.Dynamic;
using HomeAutomation.Engine.Services;

namespace HomeAutomation.Engine.Persistance
{
    public class ScheduleRepository : IScheduleRepository, IScheduleQuery
    {
        public ScheduleRepository(IViewScheduleFactory viewScheduleFactory)
        {
            this.viewScheduleFactory = viewScheduleFactory;
        }

        public static List<Schedule> Schedules = new List<Schedule>()
        {
            new Schedule()
            {
                ID = 0,
                Mon = true,
                Fri = true,
                Time = DateTime.Now,
                RoomID = 1,
                Fan = 1, Temperature = 23, Mode = Mode.Heat
            },


            new Schedule()
            {
                ID = 1,
                Mon = true,
                Fri = true,
                Sun = true,
                RoomID = 2,
                Time = DateTime.Now,
               Fan = 1, Temperature = 23, Mode = Mode.Heat
            },
        };

        static object sync = new object();
        private IViewScheduleFactory viewScheduleFactory;

        public Schedule Add(bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun, int roomID,
            DateTime time, short temperture, short fan, Mode mode)
        {
            int id = 0;

            lock (sync)
            {
                if (Schedules.Count > 0)

                {
                    id = Schedules.Max(o => o.ID) + 1;
                }
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
            lock (sync)
            {
                Schedules.Add(schedule);
            }

            return schedule;
        }

        public void Delete(int iD)
        {
            lock (sync)
            {
                var r = Schedules.Where(o => o.ID == iD).FirstOrDefault();
                Schedules.Remove(r);

            }
        }

        public IEnumerable<Schedule> GetAll()
        {
            return Schedules;
        }

        public IEnumerable<dynamic> GetFormated()
        {
            {

                List<dynamic> result = new List<dynamic>();
                foreach (var schedule in Schedules)
                {


                    result.Add(this.viewScheduleFactory.Create(schedule));
                }
                return result;
            }
        }
    }
}
