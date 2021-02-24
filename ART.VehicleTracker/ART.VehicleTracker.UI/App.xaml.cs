using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ART.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var vehicleList = serviceProvider.GetRequiredService<VehicleList>();
                Application.Current.Run(vehicleList);

            }
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<VehicleList>()
                .AddLogging(configure => configure.AddEventLog())
                .AddLogging(configure => configure.AddDebug());
        }
    }
}
