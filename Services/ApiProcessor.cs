using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomation.Engine.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace HomeAutomation.Engine.Services
{
    public class ApiProcessor : IControllerProcessor
    {
        //private ICommandBus commandBus;
        //public ApiProcessor(ICommandBus commandBus)
        //{
        //    this.commandBus = commandBus;
        //}
        public IActionResult Execute(Func<object> query)
        {
            
                var result = query();
                if (result != null)
                {
                    return new OkObjectResult(result);
                }
                else
                {
                    return new NotFoundResult();
                }
           
        }

        //public IActionResult Execute(ICommand command)
        //{
        //    try
        //    {
        //        commandBus.Execute(command);
        //        return new OkResult();
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}
    }
}
