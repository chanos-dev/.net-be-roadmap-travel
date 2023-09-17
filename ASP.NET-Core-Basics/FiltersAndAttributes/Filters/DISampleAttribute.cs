using FiltersAndAttributes.Actions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAndAttributes.Filters
{
    public class DISampleAttribute : ActionFilterAttribute
    {
        private readonly ICalculation _calculation;

        public DISampleAttribute(ICalculation calculation)
        {
            Console.WriteLine("Constructor DISampleAttribute");
            _calculation = calculation;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"DI Sample! {_calculation.Add(2023, 2024)}");
        }
    }
}
