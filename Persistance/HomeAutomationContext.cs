﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeAutomation.Engine.Models;

namespace HomeAutomation.Engine.Persistance
{
    public class HomeAutomationContext:DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<HourlyTemperature> HourlyTemperatures { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=data.db");
        }

        public void CalculateDailyTemperature()
        {
            var now = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:00:00");

            var insertSql = $"insert into hourlytemperatures (deviceID,date,value) select deviceID, strftime('%Y-%m-%d %H:00:00', date) as Date,round( sum(value) / count(*),1) as Value from temperatures where date<'{now}' group by deviceID, strftime('%Y-%m-%d %H', date)";
            var deleteSql = $"delete from temperatures where date<'{now}'";

#pragma warning disable   EF1000      
            this.Database.ExecuteSqlCommand(insertSql);
            this.Database.ExecuteSqlCommand(deleteSql);
#pragma warning restore EF1000
            this.SaveChanges();
        }

        public List<Temperature> GetDailyTemperatures(DateTime fromDate, DateTime toDate)
        {
             return this.Temperatures.FromSql($"select max(ID) ID, deviceID, strftime('%Y-%m-%d', date) as Date, round(sum(value) / count(*), 1) as Value from hourlytemperatures  where date>={fromDate} and date<{toDate} group by deviceID, strftime('%Y-%m-%d', date)").ToList();
           
        }

        public List<Temperature> GetHourlyTemperatures(DateTime fromDate, DateTime toDate)
        {
            return this.Temperatures.FromSql($"select * from hourlytemperatures  where date>={fromDate} and date<{toDate}").ToList();
            
        }



    }
}
