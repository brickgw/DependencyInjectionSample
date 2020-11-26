using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public class ManagerFactory : IFactory
    {
        public async Task<string> Create()
        {
            await Task.Run(() => Task.Delay(500));
            return "Manager Factor Created.";
        }
    }
}
