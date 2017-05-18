using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HomeAutomation.Engine.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomeAutomation.Engine.Controllers
{
    [Route("api/[controller]")]
    public class NodeEMUController : Controller
    {
      
       static Random rnd = new Random();
       
        [HttpGet]
        [Route("/api/Temperature")]
        public IActionResult GetTemperture()
        {
            var result = new { temperature = 18 + rnd.NextDouble() * 5 };
            return Ok(result);
        }

        //short Temperature 
        //short Fan 
        //Mode Mode
        //AirCondtion model

        [HttpPost]
        [Route("/api/Remote")]
        public void Post([FromForm] short temp, [FromForm] short fan, [FromForm] short mode, [FromForm] short model)
        {
            Console.WriteLine($"temp {temp} fan {fan} mode {mode} model {model}");
        }
        //[HttpPost]
        //[Route("/api/AirCond")]
        //public void Post([FromForm] string formData)
        //{
        //    Console.WriteLine($"/api/AirCond {formData}");
        //}


    }
}
