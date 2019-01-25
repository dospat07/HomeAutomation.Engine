using HomeAutomation.Engine.Models;
using HomeAutomation.Engine.Models.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Controllers
{

    [ApiController]
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
        public ActionResult<ChartData> GetDailyTemperatures(DateTime from ,DateTime to)
        { 
            var result =  this.chartsQuery.GetDailyTemperatures(from, to.AddDays(1));          
            return Ok(result);         
        }

        [HttpGet]
        [Route("api/Charts/Hourly")]
        public ActionResult<ChartData> GetHourlyTemperatures(DateTime from, DateTime to)
        {
            var result = this.chartsQuery.GetHourlyTemperatures(from, to.AddDays(1));
            return Ok(result);
        }
    }
}
