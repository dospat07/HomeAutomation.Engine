using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.CQRS;
using HomeAutomation.Engine.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAutomation.Engine.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Authorize]
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
        /// <summary>
        ///  Returns all rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Room>>Get()
        {
            Console.WriteLine(this.User);
            return Ok(roomsQuery.GetAll());
        }

        /// <summary>
        /// Return room 
        /// </summary>
        /// <param name="id">room id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            var item = roomsQuery.Get(id);
            if (item == null) return NotFound();
            return Ok(item);
         
        }
        /// <summary>
        ///  Add room 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody]AddRoomCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }
        /// <summary>
        /// Send remote command to device in room id
        /// </summary>
        /// <param name="id">room id</param>
        /// <param name="remoteCommand">remote command to device</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult SendCommand(int id,[FromBody] AirCondtionCommand  remoteCommand)
        {

            SendToConditionerCommand command = new SendToConditionerCommand() { Command =remoteCommand ,RoomID= id };           
            commandBus.Execute(command);
            return Ok();

        }
        /// <summary>
        /// Update room 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put( [FromBody] UpdateRoomCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }

        /// <summary>
        /// Delete room id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteRoomCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }
    }
}
