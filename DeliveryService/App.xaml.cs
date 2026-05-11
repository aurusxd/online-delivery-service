using DeliveryService.Data;
using DeliveryService.Repositories;
using DeliveryService.Services;
using DeliveryService.ViewModels;
<<<<<<< HEAD
=======


>>>>>>> main
using DeliveryService.Views;
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
<<<<<<< HEAD
            services.AddTransient<OrderListViewModel>();

            // View
            services.AddTransient<OrdersListView>();
            services.AddTransient<NewOrderView>();
=======
            services.AddTransient<ListCouriersViewModel>();

            // View
            services.AddTransient<ListCouriersView>();
>>>>>>> main

            // Собираем контейнер
            Services = services.BuildServiceProvider();

            // Открываем главное окно - пока затычка
<<<<<<< HEAD
            var ordersView = Services.GetRequiredService<OrdersListView>();
            ordersView.Show();
=======
            var mainWindow = Services.GetRequiredService<ListCouriersView>();
            mainWindow.Show();
>>>>>>> main
        }
    }

}