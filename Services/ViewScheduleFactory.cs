using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Services
{
    public class ViewScheduleFactory : IViewScheduleFactory
    {
        private IRoomQuery roomQuery;

        public ViewScheduleFactory(IRoomQuery roomQuery)
        {
            this.roomQuery = roomQuery;
        }

        public ViewSchedule Create(Schedule schedule)
        {

            var item = new ViewSchedule();
            item.ID = schedule.ID;
            item.Time = schedule.Time.ToString("HH:mm");
            item.Room = roomQuery.GetAll().Where(r => r.ID == schedule.RoomID).FirstOrDefault().Name;
            item.Fan = schedule.Fan;
            item.Mode = (short)schedule.Mode;
            item.Temperature = schedule.Temperature;
            string days =
            (schedule.Mon ? "Mon," : "") + (schedule.Tue ? "Tue," : "") + (schedule.Wed ? "Wed," : "") + (schedule.Thu ? "Thu," : "") +
                            (schedule.Fri ? "Fri," : "") + (schedule.Sat ? "Sat," : "") + (schedule.Sun ? "Sun," : "");
            item.Days = days.Length>0?days.Substring(0, days.Length - 1):"";
            return item;
        }
    }
}
