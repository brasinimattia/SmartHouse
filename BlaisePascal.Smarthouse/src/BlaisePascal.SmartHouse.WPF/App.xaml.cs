using System.Configuration;
using System.Data;
using System.Windows;
using BlaisePascal.SmartHouse.Domain.LuminousDevices.Repository;
using BlaisePascal.SmartHouse.Infrastructure.Repositories.Devices.Lightning.Lamps.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BlaisePascal.SmartHouse.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            services.AddLogging(cfg =>
            {
                cfg.AddDebug(); // manda i log alla finestra "Output" di Visual Studio
            });

            services.AddSingleton<ILampRepository, JsonLampRepository>();

        }

    }
}
