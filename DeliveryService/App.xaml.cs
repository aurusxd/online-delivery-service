using DeliveryService.Data;
using DeliveryService.Repositories;
using DeliveryService.Services;
using DeliveryService.ViewModels;


<<<<<<< HEAD
using DeliveryService.Views;
=======
using DeliveryService.Services;
>>>>>>> 6015ffe54926980ca9cea93f1c04f808df45d3c1
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace DeliveryService
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();


            var services = new ServiceCollection();


            // БД
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("Default")));


            // Репозитории
            services.AddScoped<OrderRepository>();
            services.AddScoped<CourierRepository>();
            services.AddScoped<ClientRepository>();
            // Сервисы
            services.AddScoped<OrderService>();
            services.AddScoped<CourierService>();

            // ViewModels
            services.AddTransient<ListCouriersViewModel>();

            // View
            services.AddTransient<ListCouriersView>();

            // Собираем контейнер
            Services = services.BuildServiceProvider();

            // Открываем главное окно - пока затычка
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }

}
