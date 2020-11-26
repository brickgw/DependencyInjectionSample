using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionSample.Controllers
{
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
}
