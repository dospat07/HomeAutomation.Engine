using HomeAutomation.Engine.Commands;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.CommandHandler
{
    public class NodeHandlers : ICommandHandler<SendToConditionerCommand>, ICommandHandler<ReadTemperatureCommand>
    {


        IEventServer eventServer;
        IRoomQuery roomQuery;
        IRoomRepository roomRepository;
        ITemperatureRepository temperatureRepository;

        public NodeHandlers(IEventServer eventServer, IRoomQuery roomQuery, IRoomRepository roomRepository, ITemperatureRepository temperatureRepository)
        {

            this.eventServer = eventServer;
            this.roomQuery = roomQuery;
            this.roomRepository = roomRepository;
            this.temperatureRepository = temperatureRepository;

        }
        public void Handle(ReadTemperatureCommand command)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var room = this.roomQuery.Get(command.RoomID);
                    var response = client.GetAsync(room.NodeAddress + "/Temperature").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        room.Temperature = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, new { temperature = 23.45f }).temperature;
                        roomRepository.UpdateTemperature(room.ID, room.Temperature);
                        temperatureRepository.Add(room.ID, (float)room.Temperature, DateTime.Now);
                        eventServer.SendToAll(EventTypes.TemperatureUpdated, new { RoomName = room.Name, Temperature = room.Temperature });
                    }
                }
                catch (Exception e)
                {
                    eventServer.SendToAll(EventTypes.Error, new { message = e.Message });
                }
            }
        }

        public void Handle(SendToConditionerCommand command)
        {

            using (var client = new HttpClient())
            {
                //  var content = new StringContent(JsonConvert.SerializeObject(command.Command), System.Text.Encoding.UTF8, "application/json");
                var room = this.roomQuery.Get(command.RoomID);
                var content = new FormUrlEncodedContent(new[]
                 {
                      new KeyValuePair<string, string>("fan", command.Command.Fan.ToString()),
                      new KeyValuePair<string, string>("mode",  ((short) command.Command.Mode).ToString()),
                      new KeyValuePair<string, string>("temp",  command.Command.Temperature.ToString()),
                      new KeyValuePair<string, string>("model", ((short) room.AirCondition).ToString())
                 });

                var response = client.PostAsync(room.NodeAddress + "/Remote", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    eventServer.SendToAll(EventTypes.Error, new { message = $"Node response {response.StatusCode}" });

                }
                else
                {
                    eventServer.SendToAll(EventTypes.CommandSend, new { Fan = command.Command.Fan, Mode = command.Command.Mode, Temp = command.Command.Temperature, Conditioner = room.AirCondition });
                }

            }
        }
    }
}
