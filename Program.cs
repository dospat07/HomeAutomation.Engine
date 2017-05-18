using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using HomeAutomation.Engine.Services;
using Microsoft.Extensions.DependencyInjection;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Commands;
using Microsoft.Extensions.Configuration;

namespace HomeAutomation.Engine
{
    public class Program
    {
        public static void Main(string[] args)
        {
 
            var host = new WebHostBuilder()

                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseConfiguration(Startup.Configuration)
                .Build();


            var intervals = Startup.Configuration.GetSection("intervals");
            var temperatureReader = host.Services.GetService<ITemperatureReader>();
            temperatureReader.Start(Int32.Parse(intervals["temperature"]));
            var schedulerExecutor = host.Services.GetService<IScheduleCommandExecutor>();
            schedulerExecutor.Start(Int32.Parse(intervals["scheduler"]));
            host.Run();
         
        }
    }
}
