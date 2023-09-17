using FiltersAndAttributes.Filters;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetDI()
        {
            Console.WriteLine("Action Method2!!");
            return Ok("get2!");
        }

        [HttpGet("middleware")]
        [MiddlewareFilter(typeof(FilterMiddlewarePipeline))]
        //[TypeFilter(typeof(CustomTypeFilter), Arguments = new object[] { "test1", "test2" }, IsReusable = true)]
        [TypeFilter(typeof(CustomTypeFilter), Arguments = new object[] { "test1", "test2" })]
        public IActionResult GetMiddleware()
        {
            Console.WriteLine("Action Method3!!");
            return Ok("get3!");
        }

        [HttpGet("exception")]
        [CustomExceptionFilter]
        public IActionResult GetException()
        {
            throw new NotImplementedException();
        }

        [HttpGet("auth1")]
        [TypeFilter(typeof(AuthorizeActionFilter), Arguments = new object[] { "read" })]
        public IActionResult GetAuth1()
        {
            return Ok("read!");
        }

        [HttpGet("auth2")]
        [TypeFilter(typeof(AuthorizeActionFilter), Arguments = new object[] { "write" })]
        public IActionResult GetAuth2()
        {
            return Ok("write!");
        }

        [HttpGet("resource")]
        [CacheResourceFilter]
        public IActionResult GetResource()
        {
            return Ok("resource!");
        }
    }
}