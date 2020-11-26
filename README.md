## Dependency Injection in .NET CORE - Factory Pattern

This project highlights a Dependency Injection feature in .NET CORE that lends itself well to implementing the Factory Pattern.

Out of the box the .NET Core Dependency Injection will allow you inject a collection of registered types without any addition setup or wrapper classes.

The following configuration will register three different concrete implementations of the IFactory interface. 
``` public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IFactory, ManagerFactory>();
            services.AddTransient<IFactory, SalesFactory>();
            services.AddTransient<IFactory, AccountsFactory>();
        }
```

This allows us to inject all of the implementations by passing IEnumerable<IFactory> as a constructor argument. It's important to note that the order in which each implementation was registered will be the order in which each implementation
shows up in the collection. 
```
private readonly IEnumerable<IFactory> _factories;

        public WeatherForecastController(IEnumerable<IFactory> factories)
        {
            _factories = factories;
        }
```

An extension method will simplify how consumers find the concrete class they are looking for. It's implemented like this....'
```
public static class FactoryExtensions
    {
        public static IFactory GetFactory<TFactory>(this IEnumerable<IFactory> factories)
        where TFactory : class, IFactory
        {
            return factories.First(factory => factory is TFactory);
        }
    }
```

Usage of this is demonstrated in the WeatherForecastController (I was too lazy to change the name for such a trivial project.)
```
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
            var sales = await _factories.GetFactory<SalesFactory>().Create();
            var accounts = await _factories.GetFactory<AccountsFactory>().Create();
            var managers = await _factories.GetFactory<ManagerFactory>().Create();

            return Ok($"{sales}\n{accounts}\n{managers}");
        }
    }
    ```