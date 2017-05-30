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
        public DbSet<Room> Rooms { get; set; }
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

            var insertSql = $"insert into hourlytemperatures (roomID,date,value) select roomID, strftime('%Y-%m-%d %H:00:00', date) as Date, sum(value) / count(*) as Value from temperatures where date<'{now}' group by roomID, strftime('%Y-%m-%d %H', date)";
            var deleteSql = $"delete from temperatures where date<'{now}'";
            Console.WriteLine(insertSql);
            Console.WriteLine(deleteSql);
            this.Database.ExecuteSqlCommand(insertSql);
            this.Database.ExecuteSqlCommand(deleteSql);
            this.SaveChanges();
        }

        
    }
}
