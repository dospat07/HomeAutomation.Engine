using HomeAutomation.Engine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Controllers
{

    
    [Authorize]
    public class ChartsController:Controller
    {
        private IChartsQuery chartsQuery;
        public ChartsController(IChartsQuery chartsQuery)
        {
            this.chartsQuery = chartsQuery;
        }

        [HttpGet]
        [Route("api/Charts/Daily")]
        public IActionResult GetDailyTemperatures(DateTime from ,DateTime to)
        { 
            var result =  this.chartsQuery.GetDailyTemperatures(from, to.AddDays(1));          
            return Ok(result);         
        }

        [HttpGet]
        [Route("api/Charts/Hourly")]
        public IActionResult GetHourlyTemperatures(DateTime from, DateTime to)
        {
            var result = this.chartsQuery.GetHourlyTemperatures(from, to.AddDays(1));
            return Ok(result);
        }
    }
}
