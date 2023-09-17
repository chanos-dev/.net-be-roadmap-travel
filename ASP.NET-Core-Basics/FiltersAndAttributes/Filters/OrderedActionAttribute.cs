using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAndAttributes.Filters
{
    public class OrderedActionAttribute : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("OrderedActionFilter OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("OrderedActionFilter OnActionExecuted");
        }
    }
}
