using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public class AccountsFactory : IFactory
    {
        public async Task<string> Create()
        {
            await Task.Run(() => Task.Delay(1000));
            return "Account Factory Created.";
        }
    }
}
