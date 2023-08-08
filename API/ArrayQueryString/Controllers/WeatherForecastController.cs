using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace ArrayQueryString.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // ?foos=hello&foos=world
        // ?foos[0]=hello&foos[1]=world
        [HttpGet]
        public IActionResult GetArrayQueryString([FromQuery] string[] foos)
        {
            return Ok(string.Join(", ", foos));
        }

        // ?foos[]=hello&foos[]=world
        [HttpGet]
        public IActionResult GetArrayQueryString2([FromQuery(Name = "foos[]")] string[] foos)
        {
            return Ok(string.Join(", ", foos));
        }


        // ?foos={ "type": "type1", "contents": "hello, world" }&foos={ "type": "type2", "contents": "hello, world2" }        
        [HttpGet]
        public IActionResult GetObjectArrayQueryString([FromQuery] Foo[] foos)
        {
            return Ok(string.Join(", ", foos.Select(foo => JsonSerializer.Serialize(foo))));
        }
        
        // ?boos[0].Id=1&boos[0].Name=hello&boos[1].Id=2&boos[1].Name=world
        [HttpGet]
        public IActionResult GetObjectArrayQueryString2([FromQuery] Boo[] boos)
        {
            return Ok(string.Join(", ", boos.Select(boo => JsonSerializer.Serialize(boo))));
        }

        // ?foos[]={ "Id":1, "Name":"Hello" }&foos[]={ "Id":2, "Name":"World" }
        [HttpGet]
        public IActionResult GetObjectArrayQueryString3([FromQuery(Name = "foos[]")] Foo[] foos)
        {
            return Ok(string.Join(", ", foos.Select(foo => JsonSerializer.Serialize(foo))));
        }
    }

    public class Boo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [ModelBinder(BinderType = typeof(FooModelBinder))]
    public class Foo
    {
        public string Type { get; set; }
        public string Contents { get; set; }
    }

    // https://learn.microsoft.com/ko-kr/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-6.0
    public class FooModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext is null)
                throw new ArgumentNullException();

            string modelName = bindingContext.ModelName;

            ValueProviderResult result = bindingContext.ValueProvider.GetValue(modelName);

            if (result == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, result);

            string? json = result.FirstValue;

            if (string.IsNullOrEmpty(json))
                return Task.CompletedTask;

            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            Foo foo = JsonSerializer.Deserialize<Foo>(json, options) ?? throw new Exception("json is invalid.");

            if (string.IsNullOrEmpty(foo.Type) ||
                string.IsNullOrEmpty(foo.Contents))
            {
                bindingContext.ModelState.TryAddModelError(modelName, "type or contents are empty.");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(foo);
            return Task.CompletedTask;
        }
    }
}