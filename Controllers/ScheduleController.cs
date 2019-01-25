﻿using HomeAutomation.Engine.CQRS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Commands;
using Microsoft.AspNetCore.Authorization;

namespace HomeAutomation.Engine.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
   
    public class ScheduleController : Controller
    {
        private ICommandBus commandBus;
        private IScheduleQuery scheduleQuery;

        public ScheduleController(ICommandBus commandBus, IScheduleQuery scheduleQuery)
        {
            this.commandBus = commandBus;
            this.scheduleQuery = scheduleQuery;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ViewSchedule>> Get()
        {
            return Ok(scheduleQuery.GetAllFormated());
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddScheduleCommand command)
        {
            
            commandBus.Execute(command);
            return Ok();
        }

      
        [HttpDelete]
        public IActionResult Delete([FromBody] DeleteScheduleCommand command)
        {
            commandBus.Execute(command);
            return Ok();
        }
    }
}
