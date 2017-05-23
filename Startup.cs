using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Persistance;
using Newtonsoft.Json.Serialization;
using HomeAutomation.Engine.Services;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.CommandHandler;
using HomeAutomation.Engine.Commands;
using Microsoft.Extensions.Configuration;
using System.IO;
//using HomeAutomation.Engine.Identity;

namespace HomeAutomation.Engine
{
    public class Startup
    {


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            services.AddCors();
           
            services.AddSignalR();

            services.AddEntityFrameworkSqlite().AddDbContext<HomeAutomationContext>();

            services.AddCookieAuthentication(o=>o.CookieHttpOnly = false);
            //
            // IdentityServer 4
            //
            //services.AddIdentityServer()
            //  .AddTemporarySigningCredential()
            //  .AddInMemoryApiResources(Config.GetApiResources())
            //  .AddInMemoryClients(Config.GetClients());

            //cqrs
            services.AddSingleton<IContainerResolver, Resolver>();
            services.AddSingleton<ICommandBus, CommandBus>();


            //data
            services.AddScoped<IRoomQuery, RoomDbRepository>();
            services.AddScoped<IRoomRepository, RoomDbRepository>();
            services.AddScoped<IScheduleQuery, ScheduleDbRepository>();
            services.AddScoped<IScheduleRepository, ScheduleDbRepository>();


            //services
            services.AddSingleton<ITemperatureReader, TemperatureReader>();
            services.AddSingleton<IScheduleCommandExecutor, ScheduleCommandExecutor>();
            services.AddSingleton<IEventServer, EventServer>();
            services.AddScoped<IViewScheduleFactory, ViewScheduleFactory>();

            // command handlers
            services.AddScoped<ICommandHandler<AddRoomCommand>, RoomHandlers>();
            services.AddScoped<ICommandHandler<DeleteRoomCommand>, RoomHandlers>();
            services.AddScoped<ICommandHandler<UpdateRoomCommand>, RoomHandlers>();

            services.AddScoped<ICommandHandler<AddScheduleCommand>, ScheduleHandlers>();
            services.AddScoped<ICommandHandler<DeleteScheduleCommand>, ScheduleHandlers>();


            services.AddScoped<ICommandHandler<ReadTemperatureCommand>, NodeHandlers>();
            services.AddScoped<ICommandHandler<SendToConditionerCommand>, NodeHandlers>();


           
           


        }

        private static IConfigurationRoot configuration = null;
        public static IConfigurationRoot Configuration
        {

            get
            {
                if (configuration == null)
                {
                    var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                    configuration = configurationBuilder.Build();
                }
                return configuration;

            }

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = Configuration["authorityServer"],
            //    RequireHttpsMetadata = false,

            //    ApiName = "engine"
            //});
            List<string> origins = new List<string>();
            foreach (var item in Configuration.GetSection("origins").GetChildren())
            {
                origins.Add(item.Value);
            }


            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins(origins.ToArray()).AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();

            app.UseWebSockets();

            app.UseSignalR(r=> 
            {
                r.MapHub<EngineHub>("/socket");
            });


            //
            // IdentityServer 4
            //
            // app.UseIdentityServer();

            using (var db = new HomeAutomationContext())
            {
                db.Database.EnsureCreated();

            }

        }
    }
}
