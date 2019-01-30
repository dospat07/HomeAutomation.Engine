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
    public class DevicesController : Controller
    {
        ICommandBus commandBus;
        IDeviceQuery deviceQuery;
        public DevicesController(ICommandBus commandBus,IDeviceQuery deviceQuery)
        {
            this.commandBus = commandBus;
            this.deviceQuery = deviceQuery;
        }
        /// <summary>
        ///  Returns all devices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Device>>Get()
        {
            Console.WriteLine(this.User);
            return Ok(deviceQuery.GetAll());
        }

        /// <summary>
        /// Return device 
        /// </summary>
        /// <param name="id">device id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Device> Get(int id)
        {
            var item = deviceQuery.Get(id);
            if (item == null) return NotFound();
            return Ok(item);
         
        }
        /// <summary>
        ///  Add device 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public IActionResult Post([FromBody]AddDeviceCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }
        /// <summary>
        /// Send remote command to device  
        /// </summary>
        /// <param name="id">device id</param>
        /// <param name="remoteCommand">remote command to device</param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult SendCommand(int id,[FromBody] ApplianceCommand  remoteCommand)
        {

            SendToConditionerCommand command = new SendToConditionerCommand() { Command =remoteCommand ,DeviceID= id };           
            commandBus.Execute(command);
            return Ok();

        }
        /// <summary>
        /// Update device
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put( [FromBody] UpdateDeviceCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }

        /// <summary>
        /// Delete device id
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteDeviceCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }
    }
}
