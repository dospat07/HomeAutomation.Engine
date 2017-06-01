using HomeAutomation.Engine.CQRS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Services
{
     public interface IControllerProcessor
    {
        IActionResult Execute(Func<object> query);
      //  IActionResult Execute(ICommand command);
    }
}
