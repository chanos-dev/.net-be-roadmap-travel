namespace Middleware.Middlewares
{
    // IMiddleware는 클라이언트 요청(연결)마다 활성화되므로 범위가 지정된 서비스는 미들웨어의 생성자에 주입될 수 있습니다.
    public class FactoryMiddleware : IMiddleware
    {
        private readonly IFooCtor _ctor;

        public FactoryMiddleware(IFooCtor ctor)
        {
            _ctor = ctor;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _ctor.Print();

            await next(context);
        }
    }

    public interface IFooCtor
    {
        public void Print();
    }

    public class FooCtor : IFooCtor
    {
        public void Print() => Console.WriteLine("Foo Constroctor DI!!");
    }

    public static class FactoryMiddlewareExntensions
    {
        public static WebApplicationBuilder AddFactoryDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFooCtor, FooCtor>();

            builder.Services.AddScoped<FactoryMiddleware>();

            return builder;
        }

        public static IApplicationBuilder AddFactoryMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<FactoryMiddleware>();

            return app;
        }
    }
}
