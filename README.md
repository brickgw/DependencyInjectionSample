## Dependency Injection in .NET CORE - Factory Pattern

This project highlights a Dependency Injection feature in .NET CORE that allows us inject a collection of registered types without any addition setup or wrapper classes.

The following configuration will register three different concrete implementations of the IFactory interface. 

```cs
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IFactory, ManagerFactory>();
            services.AddTransient<IFactory, SalesFactory>();
            services.AddTransient<IFactory, AccountsFactory>();
        }
```

This allows us to inject all of the implementations by passing IEnumerable<IFactory> as a constructor argument. It's important to note that the order in which each implementation was registered will be the order in which each implementation
shows up in the collection. 
```cs
        private readonly IEnumerable<IFactory> _factories;

        public WeatherForecastController(IEnumerable<IFactory> factories)
        {
            _factories = factories;
        }
```

An extension method will simplify how consumers find the concrete class they are looking for. It's implemented like this....'
```cs
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
```

Usage of this is demonstrated in the WeatherForecastController (I was too lazy to change the name for such a trivial project.)

```cs
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IEnumerable<IFactory> _factories;

        public WeatherForecastController(IEnumerable<IFactory> factories)
        {
            _factories = factories;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sales = await _factories.SalesFactory().Create();
            var accounts = await _factories.AccountsFactory().Create();
            var managers = await _factories.ManagerFactory().Create();

            return Ok($"{sales}\n{accounts}\n{managers}");
        }
    }
```