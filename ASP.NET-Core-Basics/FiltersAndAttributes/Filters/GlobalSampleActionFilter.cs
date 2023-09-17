using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAndAttributes.Filters
{
    public class GlobalSampleActionFilter : ActionFilterAttribute
    {
        public GlobalSampleActionFilter()
        {
            Console.WriteLine("Constructor GlobalSampleActionFilter");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("GlobalSampleActionFilter OnActionExecuted");

            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("GlobalSampleActionFilter OnActionExecuting");

            base.OnActionExecuting(context);
        }
    }
}
