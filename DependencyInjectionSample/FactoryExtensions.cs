using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample
{
    public static class FactoryExtensions
    {
        public static IFactory SalesFactory(this IEnumerable<IFactory> factories)
        {
            return factories.First(x => x is SalesFactory);
        }

        public static IFactory ManagerFactory(this IEnumerable<IFactory> factories)
        {
            return factories.First(x => x is ManagerFactory);
        }

        public static IFactory AccountsFactory(this IEnumerable<IFactory> factories)
        {
            return factories.First(x => x is AccountsFactory);
        }
    }
}
