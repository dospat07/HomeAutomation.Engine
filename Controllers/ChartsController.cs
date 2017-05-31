using HomeAutomation.Engine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    public class ChartsController:Controller
    {
        private IChartsQuery chartsQuery;
        public ChartsController(IChartsQuery chartsQuery)
        {
            this.chartsQuery = chartsQuery;
        }

        [HttpGet]
        public IActionResult GetTemperatures(DateTime from ,DateTime to)
        {
            var result =  this.chartsQuery.GetTemperatures(from, to.AddDays(1));
            if (result!=null)
                return Ok(result);
            return NotFound();
        }
    }
}
