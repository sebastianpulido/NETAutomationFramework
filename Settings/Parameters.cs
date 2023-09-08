namespace Settings;

using Microsoft.Extensions.Configuration;
using System;
using System.IO;

public class Parameters
{
    public IConfigurationRoot configuration;

    public Parameters()
    {
            configuration =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", true)
            .Build();
    }
}