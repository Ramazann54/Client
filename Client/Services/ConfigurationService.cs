using Microsoft.Extensions.Configuration;
using System;

namespace Client.Services
{
    public class ConfigurationService
    {
        public string ServerAddress { get; private set; }

        public ConfigurationService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            ServerAddress = configuration["ClientSettings:ServerAddress"];
        }
    }
}
