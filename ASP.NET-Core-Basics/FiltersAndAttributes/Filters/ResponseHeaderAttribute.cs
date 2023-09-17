using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAndAttributes.Filters
{
    public class ResponseHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public ResponseHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Response Header Filter! {_name} / {_value}");

            context.HttpContext.Response.Headers.Add(_name, _value);

            base.OnActionExecuting(context);
        }
    }
}
