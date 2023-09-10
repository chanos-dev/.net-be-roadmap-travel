using Microsoft.Extensions.Options;
using SettingsAndConfigurations.Settings;

namespace SettingsAndConfigurations.Configurations
{
    internal static class Startup
    {
        internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            string configurationPath = "Configurations";
            string environmentName = builder.Environment.EnvironmentName;

            builder.Configuration.AddJsonFile($"{configurationPath}/foo.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationPath}/foo.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationPath}/user.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationPath}/user.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"{configurationPath}/diff.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{configurationPath}/diff.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables(); // 환경 변수 세팅

            builder.Services.Configure<FooSettings>(builder.Configuration.GetSection(nameof(FooSettings)))
                .Configure<DiffSettings>(builder.Configuration.GetSection(nameof(DiffSettings)));

            foreach (var c in builder.Configuration.AsEnumerable())
                Console.WriteLine(c.Key + " = " + c.Value);

            return builder;
        }

        internal static WebApplicationBuilder AddNamedConfigurations(this WebApplicationBuilder builder)
        { 
            // 옵션 유효성 검사            
            builder.Services.AddOptions<UserSettings>()                
                .BindConfiguration(nameof(UserSettings))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddSingleton<IConfigureOptions<NamedBooSettings>, ConfigureNamedBooSettings>();

            return builder;
        }
    }
}
