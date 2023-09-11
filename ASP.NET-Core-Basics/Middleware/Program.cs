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

#region Use�� �̿��� Middleware ����
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

    // response�� ������ �� HttpResponse�� �����ϴ� ��� throw�� �߻��Ѵ�.
    // �߸��� ����
    // await context.Response.WriteAsync("Hello, World!");
    // await next.Invoke();    
    // - �������� ������ �߻��� �� �ִ�. ex) ��õ� Content-Length ���� �� ������ �ۼ��� �� �ִ�.
    // - ���� ������ �ջ��ų �� �ִ�.
    // HasStarted �Ӽ��� ���� response�� ���� �ƴ��� Ȯ���� �� �ִ�.
    if (!context.Response.HasStarted)
    {
        Console.WriteLine("context response has not started.");
        await context.Response.WriteAsJsonAsync(new
        {
            Response = "Hello, world!",
        });
    }
});

// �̵��� �ܶ�(short-circuit)�Ǵ� ��� �̵����� ���� ��û�� ó������ ���ϵ��� �ϱ� ������ �̸� �͹̳� �̵������ �Ѵ�.
//app.Use((HttpContext context, RequestDelegate next) =>
//{
//    Console.WriteLine("short-circuiting!");

//    return Task.CompletedTask;
//});

#endregion

#region ���� ��û delegate, ��� HTTP Request�� ���� �������� ȣ��ȴ�. (controller���� ���� ����)
// Run �븮�ڴ� next �Ű� ������ ���� �ʴ´�.
// Run �븮�ڴ� �׻� �͹̳� �̵�����̸� ������������ �����Ѵ�.
// �Ϻ� �̵���� ���� ��Ҵ� ������������ ������ ����Ǵ� `Run[Middleware]` �޼��带 ������ �� �ִ�.
// Run �븮�� �ڿ� �߰��� �ٸ� `Use` �Ǵ� `Run` �븮�ڴ� ȣ����� �ʴ´�.

//app.Run(async context =>
//{
//    Console.WriteLine("run delegate!");
//    await context.Response.WriteAsync("Hello, World!");
//});

// ȣ�� �ȵ�
//app.Use(async (context, next) =>
//{
//    Console.WriteLine("run next!");
//    await next();
//});
#endregion

#region Map�� �̿��� Middleware �б�

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

#region TODO : MSDN ���� �ʿ�
// �̵���� ���� ��Ұ� ���Ͽ� �߰��Ǵ� ������ ��û���� �̵���� ���� ��Ұ� ȣ��Ǵ� ������ ���信 ���� �����̴�.
// ������ ����, ���� �� ��ɿ� �߿��ϴ�.
// ���� ���� �̵���� ���� ��� ���� ����
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

// UseCors, UseAuthentication, UseAuthorization�� ǥ�õ� ������ ǥ�õǾ�� �Ѵ�.
// UseCors�� UseResponseCaching���� ���� ǥ�õǾ�� �Ѵ�.
// UseRequestLocalization�� ��û ��ȭ(Culture)�� Ȯ���� �� �ִ� �̵����, ���� ��� app.UseStaticFiles()�� ���� �̵���� �տ� ��Ÿ���� �մϴ�.
// UseRateLimiter�� ������� ����� �� ��������Ʈ���� ��û ���� ���� API�� ����ϴ� ��� UseRouting ���Ŀ� ȣ��Ǿ�� �մϴ�. ���� ���, [EnableRateLimiting] �Ӽ��� ����ϴ� ��� UseRateLimiter�� UseRouting ���Ŀ� ȣ��Ǿ�� �մϴ�. ���� ���� ���ѱ⸸ ����ϴ� ��� UseRateLimiter�� UseRouting ������ ȣ���� �� �ֽ��ϴ�.
#endregion