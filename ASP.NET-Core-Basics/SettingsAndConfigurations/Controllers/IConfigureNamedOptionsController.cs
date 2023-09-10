using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SettingsAndConfigurations.Settings;

namespace SettingsAndConfigurations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IConfigureNamedOptionsController : ControllerBase
    { 
        private readonly ILogger<IConfigureNamedOptionsController> _logger;

        private readonly NamedBooSettings _googleNamedBooSettings;
        private readonly NamedBooSettings _localhostNamedBooSettings;

        public IConfigureNamedOptionsController(ILogger<IConfigureNamedOptionsController> logger, IOptionsMonitor<NamedBooSettings> options)
        {
            _logger = logger;
            _localhostNamedBooSettings = options.Get("localhost");
            _googleNamedBooSettings = options.Get("google");
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                localhost = _localhostNamedBooSettings,
                google = _googleNamedBooSettings,
            });
        }
    }
}