using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersAndAttributes.Filters
{
    public class CustomTypeFilter : IActionFilter
    {
        private readonly IFoo _foo;
        private readonly string _test1;
        private readonly string _test2;
        public CustomTypeFilter(IFoo foo, string test1, string test2)
        {
            _foo = foo;
            _test1 = test1;
            _test2 = test2;
            Console.WriteLine("Constructor CustomTypeFilter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"CustomTypeFilter OnActionExecuted {_test1} / {_test2}");
            _foo.Print();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"CustomTypeFilter OnActionExecuted {_test1} / {_test2}");
            _foo.Print();
        }
    }

    public interface IFoo
    {
        void Print();
    }

    public class Foo : IFoo
    {
        public void Print()
        {
            Console.WriteLine("Foo!");
        }
    }
}
