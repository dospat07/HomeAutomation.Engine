using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Commands;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAutomation.Engine.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        ICommandBus commandBus;
        IRoomQuery roomsQuery;
        public RoomsController(ICommandBus commandBus,IRoomQuery roomsQuery)
        {
            this.commandBus = commandBus;
            this.roomsQuery = roomsQuery;
        }
         
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            Console.WriteLine(this.User);
            return roomsQuery.GetAll();
        }

       
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = roomsQuery.Get(id);
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
        }
         
        [HttpPost]
        public void Post([FromBody]AddRoomCommand command)
        {
            commandBus.Execute(command);
        }

        [HttpPost("{id}")]
        public IActionResult SendCommand(int id,[FromBody] AirCondtionCommand  cmd)
        {
             
            SendToConditionerCommand command = new SendToConditionerCommand() { Command =cmd ,RoomID= id };           
            commandBus.Execute(command);
            return Ok();

        }

        [HttpPut]
        public IActionResult Put( [FromBody] UpdateRoomCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }

       
        [HttpDelete]
        public void Delete([FromBody] DeleteRoomCommand command)
        {
            commandBus.Execute(command);
        }
    }
}
