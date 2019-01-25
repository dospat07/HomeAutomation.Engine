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
    /// <summary>
    ///Return data for chart control
    /// </summary>
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    [Authorize]
    public class ChartsController:Controller
    {
        private readonly IChartsQuery chartsQuery;
        public ChartsController(IChartsQuery chartsQuery)
        {
            this.chartsQuery = chartsQuery;
        }

        /// <summary>
        /// Returns temperature for period on daily a basis
        /// </summary>
        /// <param name="from">Start date</param>
        /// <param name="to">End date</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Charts/Daily")]
        public ActionResult<ChartData> GetDailyTemperatures(DateTime from ,DateTime to)
        { 
            var result =  this.chartsQuery.GetDailyTemperatures(from, to.AddDays(1));          
            return Ok(result);         
        }
        /// <summary>
        /// Returns temperature for period on hourly a basis
        /// </summary>
        /// <param name="from">Start date</param>
        /// <param name="to">End date</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Charts/Hourly")]
        public ActionResult<ChartData> GetHourlyTemperatures(DateTime from, DateTime to)
        {
            var result = this.chartsQuery.GetHourlyTemperatures(from, to.AddDays(1));
            return Ok(result);
        }
    }
}
