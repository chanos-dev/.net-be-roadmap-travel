using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SettingsAndConfigurations.Settings;

namespace SettingsAndConfigurations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IOptionsController : ControllerBase
    { 
        private readonly ILogger<IOptionsController> _logger;

        private readonly FooSettings _fooSettings;

        private readonly DiffSettings _monitorDiffSettings;
        private readonly DiffSettings _snapshotDiffSettings; 

        public IOptionsController(ILogger<IOptionsController> logger,
            IOptions<FooSettings> options,
            IOptionsMonitor<DiffSettings> monitor,
            IOptionsSnapshot<DiffSettings> snapshot)
        {
            _logger = logger;
            _fooSettings = options.Value;
            _monitorDiffSettings = monitor.CurrentValue;
            _snapshotDiffSettings = snapshot.Value; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_fooSettings);
        }

        [HttpGet("Diff")]
        public IActionResult DiffOptions()
        {
            return Ok(new
            {
                controllerMonitor = _monitorDiffSettings.Options,
                controllerSnapshot = _snapshotDiffSettings.Options,
            });
        }
    }
}