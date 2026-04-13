using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;
using MCS_MyTravel.Data;
using Microsoft.EntityFrameworkCore;
using MCS_MyTravel.Repositories;
using MCS_MyTravel.Services;
using MCS_MyTravel.ViewModel;

namespace MCS_MyTravel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(connectionString));

                   
                    services.AddScoped<IClientRepo, ClientRepo>();
                    services.AddScoped<IClientServices, ClientServices>();

                    services.AddScoped<IBookingRepo, BookingRepo>();
                    services.AddScoped<IBookingServices, BookingServices>();

                    services.AddScoped<MainViewModel>();
                    services.AddTransient<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (AppHost == null)
            {
                return;
            }
            
            await AppHost.StartAsync();

            using (var scope = AppHost.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();
            }

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (AppHost == null)
            {
                return;
            }

            await AppHost.StopAsync();
            AppHost.Dispose();
            base.OnExit(e);
        }
    }
}
