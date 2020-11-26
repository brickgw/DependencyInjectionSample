using System;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public class SalesFactory : IFactory
    {
        public async Task<string> Create()
        {
            await Task.Run(() => Task.Delay(1000));
            return "Sales Factory Created.";
        }
    }
}