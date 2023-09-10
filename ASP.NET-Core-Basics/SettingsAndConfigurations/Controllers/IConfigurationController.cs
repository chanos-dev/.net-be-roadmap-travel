using Microsoft.AspNetCore.Mvc;
using SettingsAndConfigurations.Settings;

namespace SettingsAndConfigurations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IConfigurationController : ControllerBase
    { 
        private readonly ILogger<IConfigurationController> _logger;

        private readonly IConfiguration _configuration;

        public IConfigurationController(ILogger<IConfigurationController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var notfoundKey1 = _configuration[$"{nameof(FooSettings)}:notfound"];
            var notfoundKey2 = _configuration.GetValue<int>($"{nameof(FooSettings)}:notfound");

            return Ok(new
            {
                environment = _configuration.GetValue<string>($"{nameof(FooSettings)}:{nameof(FooSettings.Environment)}"),
                key = _configuration[$"{nameof(FooSettings)}:{nameof(FooSettings.Key)}"],
                numberIndexer = _configuration[$"{nameof(FooSettings)}:{nameof(FooSettings.Number)}"],
                numberMethod = _configuration.GetValue<int>($"{nameof(FooSettings)}:{nameof(FooSettings.Number)}"),
                notfoundKey1,
                notfoundKey2,
                path = _configuration["Path"],
            });
        }
    }
}