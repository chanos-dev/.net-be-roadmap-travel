namespace Middleware.Middlewares
{
    // 미들웨어 클래스는 다음을 포함해야 한다
    // RequestDelegate 형식의 매개변수가 있는 생성자
    // Invoke 또는 InvokeAsync라는 pbulic 메서드
    //   이 메서드는 Task를 반환해야하고 HttpContext 타입을 첫 번째 매개변수로 받아야 한다.
    // 생성자 및 매개변수는 DI에 의해 채워진다.

    // 미들웨어는 애플리케이션 수명당 한번 생성된다 (singleton)

    // Invoke 메서드에서 DI를 받을 수 있다.
    public class FooMiddleware
    {
        private readonly RequestDelegate _next;

        public FooMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task InvokeAsync(HttpContext context, IFooParameter fooParameter)
        {
            Console.WriteLine("Foo Middleware!!");
            fooParameter.Print();

            await _next(context);
        }
    } 

    public interface IFooParameter
    {
        public void Print();
    }

    public class FooParameter : IFooParameter
    {
        public void Print() => Console.WriteLine("Foo Parameter DI!!");
    }

    public static class FooMiddlewareExntensions
    {
        public static WebApplicationBuilder AddFooDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFooParameter, FooParameter>();

            return builder;
        }

        public static IApplicationBuilder AddFooMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(FooMiddleware));

            return app;
        }
    }
}
