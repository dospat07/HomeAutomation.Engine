using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Services
{
    public class ViewScheduleFactory : IViewScheduleFactory
    {
        private IDeviceQuery deviceQuery;

        public ViewScheduleFactory(IDeviceQuery deviceQuery)
        {
            this.deviceQuery = deviceQuery;
        }

        public ViewSchedule Create(Schedule schedule)
        {

            var item = new ViewSchedule
            {
                ID = schedule.ID,
                Time = schedule.Time.ToString("HH:mm"),
                Name = deviceQuery.GetAll().Where(r => r.ID == schedule.DeviceID).FirstOrDefault().Name,
                Fan = schedule.Fan,
                Mode = (short)schedule.Mode,
                Temperature = schedule.Temperature
            };
            string days =
            (schedule.Mon ? "Mon," : "") + (schedule.Tue ? "Tue," : "") + (schedule.Wed ? "Wed," : "") + (schedule.Thu ? "Thu," : "") +
                            (schedule.Fri ? "Fri," : "") + (schedule.Sat ? "Sat," : "") + (schedule.Sun ? "Sun," : "");
            item.Days = days.Length>0?days.Substring(0, days.Length - 1):"";
            return item;
        }
    }
}
