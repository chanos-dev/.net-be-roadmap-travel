using Middleware.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddFooDI();
builder.AddFactoryDI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region custom middleware

// app.UseMiddleware(typeof(FooMiddleware));
app.AddFooMiddleware();
app.AddFactoryMiddleware();
app.UseMiddleware<BooMiddleware>();

#endregion

#region Use를 이용한 Middleware 연결
app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 1");
    await next();
});

app.UseWhen(context => context.Request.Query.ContainsKey("usewhen"), app =>
{
    // https://localhost:7200/WeatherForecast?usewhen=%22*hello%22
    app.Use(async (context, next) =>
    {
        Console.WriteLine("use when!");
        await next();
    });
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Middleware 2");

    await next();

    // response가 생성된 후 HttpResponse를 변경하는 경우 throw가 발생한다.
    // 잘못된 예시
    // await context.Response.WriteAsync("Hello, World!");
    // await next.Invoke();    
    // - 프로토콜 위반이 발생할 수 있다. ex) 명시된 Content-Length 보다 긴 내용이 작성될 수 있다.
    // - 본문 형식을 손상시킬 수 있다.
    // HasStarted 속성을 통해 response가 생성 됐는지 확인할 수 있다.
    if (!context.Response.HasStarted)
    {
        Console.WriteLine("context response has not started.");
        await context.Response.WriteAsJsonAsync(new
        {
            Response = "Hello, world!",
        });
    }
});

// 미들웨어가 단락(short-circuit)되는 경우 미들웨어에서 더는 요청을 처리하지 못하도록 하기 때문에 이를 터미널 미들웨어라고 한다.
//app.Use((HttpContext context, RequestDelegate next) =>
//{
//    Console.WriteLine("short-circuiting!");

//    return Task.CompletedTask;
//});

#endregion

#region 단일 요청 delegate, 모든 HTTP Request에 대한 응답으로 호출된다. (controller까지 가지 않음)
// Run 대리자는 next 매개 변수를 받지 않는다.
// Run 대리자는 항상 터미널 미들웨어이며 파이프라인을 종료한다.
// 일부 미들웨어 구성 요소는 파이프라인의 끝에서 실행되는 `Run[Middleware]` 메서드를 노출할 수 있다.
// Run 대리자 뒤에 추가된 다른 `Use` 또는 `Run` 대리자는 호출되지 않는다.

//app.Run(async context =>
//{
//    Console.WriteLine("run delegate!");
//    await context.Response.WriteAsync("Hello, World!");
//});

// 호출 안됨
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("run next!");
//    await next();
//});
#endregion

#region Map을 이용한 Middleware 분기

app.Map("/WeatherForecast", app =>
{
    app.Use(async (context, next) =>
    {
        Console.WriteLine("/WeatherForecast Map Middleware 1");
        await next();
    });

    app.Use(async (context, next) =>
    {
        Console.WriteLine("/WeatherForecast Map Middleware 2");
        await next();
    });

    app.Run(async context =>
    {
        await context.Response.WriteAsync("end!");
    });
});

app.Map("/WeatherForecast/two", app =>
{
    app.Use(async (context, next) =>
    {
        Console.WriteLine("/WeatherForecast/two Map Middleware 1");
        await next();
    });

    app.Use(async (context, next) =>
    {
        Console.WriteLine("/WeatherForecast/two Map Middleware 2");
        await next();
    });

    app.Run(async context =>
    {
        await context.Response.WriteAsync("end!");
    }); 
});

app.MapWhen(context => context.Request.Query.ContainsKey("when"), app =>
{
    // https://localhost:7200/test/?when=%22hello!%22
    app.Run(async context =>
    {
        await context.Response.WriteAsync("when end!");
    });
});

#endregion

app.Run();

#region TODO : MSDN 정리 필요
// 미들웨어 구성 요소가 파일에 추가되는 순서는 요청에서 미들웨어 구성 요소가 호출되는 순서와 응답에 대한 역순이다.
// 순서는 보안, 성능 및 기능에 중요하다.
// 보안 관련 미들웨어 구성 요소 권장 순서
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//// app.UseCookiePolicy();

//app.UseRouting();
//// app.UseRateLimiter();
//// app.UseRequestLocalization();
//// app.UseCors();

//app.UseAuthentication();
//app.UseAuthorization();
//// app.UseSession();
//// app.UseResponseCompression();
//// app.UseResponseCaching();

// UseCors, UseAuthentication, UseAuthorization은 표시된 순서로 표시되어야 한다.
// UseCors는 UseResponseCaching보다 먼저 표시되어야 한다.
// UseRequestLocalization은 요청 문화(Culture)를 확인할 수 있는 미들웨어, 예를 들어 app.UseStaticFiles()와 같은 미들웨어 앞에 나타나야 합니다.
// UseRateLimiter는 라우팅을 사용할 때 엔드포인트별로 요청 비율 제한 API를 사용하는 경우 UseRouting 이후에 호출되어야 합니다. 예를 들어, [EnableRateLimiting] 속성을 사용하는 경우 UseRateLimiter는 UseRouting 이후에 호출되어야 합니다. 전역 비율 제한기만 사용하는 경우 UseRateLimiter는 UseRouting 이전에 호출할 수 있습니다.
#endregion