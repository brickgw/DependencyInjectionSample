using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public interface IFactory
    {
        Task<string> Create();
    }
}
