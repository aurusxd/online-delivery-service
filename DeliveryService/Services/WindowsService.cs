using DeliveryService.Views;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.Services
{
    /// <summary>
    /// Сервис, открывающий окна
    /// </summary>
    public class WindowsService
    {
        private readonly IServiceProvider _services;


        public WindowsService(IServiceProvider services)
        {
            _services = services;
        }


        /// <summary>
        /// Открытие OrderListView
        /// </summary>
        public void OpenOrderList()
        {
            var win = _services.GetRequiredService<OrdersListView>();
            win.Show();
        }
        /// <summary>
        /// Открытие ListCouriersView
        /// </summary>
        public void OpenListCouriers()
        {
            var win = _services.GetRequiredService<ListCouriersView>();
            win.Show();
        }
        /// <summary>
        /// Открытие DispatcherView
        /// </summary>
        public void OpenDispatcher()
        {
            var win = _services.GetRequiredService<DispatcherView>();
            win.Show();
        }
        /// <summary>
        /// Открытие NewOrderView
        /// </summary>
        /// <returns>Результат работы окна - DialogResult</returns>
        public bool? OpenNewOrder()
        {
            var win = _services.GetRequiredService<NewOrderView>();
            return win.ShowDialog();
        }
        /// <summary>
        /// Открытие RegistrationCourier
        /// </summary>
        /// <returns>Результат работы окна - DialogResult</returns>
        public bool? OpenRegistrationCourier()
        {
            var win = _services.GetRequiredService<RegistrationCourier>();
            return win.ShowDialog();
        }
    }
}