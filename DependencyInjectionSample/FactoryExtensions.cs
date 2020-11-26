using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public static class FactoryExtensions
    {
        public static IFactory GetFactory<TFactory>(this IEnumerable<IFactory> factories)
        where TFactory : class, IFactory
        {
            return factories.First(factory => factory is TFactory);
        }
    }
}
