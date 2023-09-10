using Microsoft.Extensions.Options;

namespace SettingsAndConfigurations.Settings
{
    public class ConfigureNamedBooSettings : IConfigureNamedOptions<NamedBooSettings>
    {
        private UserSettings _userSettings;

        public ConfigureNamedBooSettings(IOptions<UserSettings> options)
        {
            _userSettings = options.Value;
        }

        public void Configure(string name, NamedBooSettings options)
        {
            if (string.IsNullOrEmpty(name))
                return;

            if (name == "localhost")
            {
                options.Host = $"http://localhost?id={_userSettings.Id}&pw={_userSettings.Password}";
                options.Port = 80;
            }

            if (name == "google")
            {
                options.Host = $"https://google.com?gid={_userSettings.Id}&gpw={_userSettings.Password}";
                options.Port = 80;
            }
        }

        public void Configure(NamedBooSettings options)
            => Configure(string.Empty, options);
    }
}
