using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAndAttributes.Filters
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    { 
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("CustomExceptionFilter OnException");
            context.ExceptionHandled = true;
            context.Result = new ContentResult
            {
                Content = "error~",
                StatusCode = StatusCodes.Status400BadRequest,
            };
        }
    }
}
