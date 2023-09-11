namespace Middleware.Middlewares
{ 
    public class BooMiddleware
    {
        private readonly RequestDelegate _next;

        public BooMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Boo Middleware!!");

            await _next(context);
        }
    }
}
