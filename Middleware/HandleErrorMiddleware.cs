using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Middleware
{
    public class HandleErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public HandleErrorMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
               
                await _next(context);
              

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"The following error happened: {e.Message}");
            }
        }
    }
}
