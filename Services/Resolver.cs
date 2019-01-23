using HomeAutomation.Engine.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomation.Engine.Services
{
    public class Resolver : IContainerResolver
    {


        private readonly IServiceProvider serviceProvider;
        public Resolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

    
        public object  GetService(Type type)
        {
            return this.serviceProvider.GetService(type);
        }
    }
}
