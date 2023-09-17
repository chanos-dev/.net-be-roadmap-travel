using FiltersAndAttributes.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace FiltersAndAttributes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ResponseHeader("Filter-Header", "Filter Value")]
    [OrderedAction(Order = int.MinValue)]
    public class FilterController : ControllerBase
    { 
        private readonly ILogger<FilterController> _logger;

        public FilterController(ILogger<FilterController> logger)
        {
            _logger = logger;
            Console.WriteLine("Constructor FilterController");
        }

        [HttpGet]
        [ResponseHeader("Another-Filter-Header", "Another-Filter Value")]
        public IActionResult Get()
        {
            Console.WriteLine("Action Method!");
            return Ok("get!");
        }

        [HttpGet("di")]
        [ServiceFilter(typeof(DISampleAttribute))]
        public IActionResult Get2()
        {
            Console.WriteLine("Action Method2!!");
            return Ok("get2!");
        }

        [HttpGet("middleware")]
        [MiddlewareFilter(typeof(FilterMiddlewarePipeline))]
        public IActionResult Get3()
        {
            Console.WriteLine("Action Method3!!");
            return Ok("get3!");
        }
    }
}