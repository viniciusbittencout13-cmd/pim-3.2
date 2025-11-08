using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using GLLRV.DesktopApp.Services;

namespace GLLRV.DesktopApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; } = null!;
        public static IDataStore DataStore { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var dataRoot = Configuration["DataFolder"] ?? "data";
            DataStore = new JsonDataStore(dataRoot);
        }
    }
}
