using System;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=data.db");
        }
    }
}
