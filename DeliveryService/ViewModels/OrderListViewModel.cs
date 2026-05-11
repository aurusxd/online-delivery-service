using DeliveryService.Commands;
using DeliveryService.Models;
using DeliveryService.Services;
using DeliveryService.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeliveryService.ViewModels
{
    /// <summary>
    /// Класс-DTO для отображения в окне
    /// </summary>
    public class OrderItem
    {
        public int Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string OrderTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// Логика взаимодействия пользователя и базы данных с OrderListView
    /// </summary>
    public class OrderListViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;

        /// <summary>
        /// Список DTO всех заказов
        /// </summary>
        private ObservableCollection<OrderItem> _orders;
        
        /// <summary>
        /// Количество заказов
        /// </summary>
        private int _totalCount;
        /// <summary>
        /// Количество заказов со статусом "В процессе" (поменять в кавычках на название в проекте)
        /// </summary>
        private int _inProcessCount;
        /// <summary>
        /// Количество заказов со статусом "Ожидают" (поменять в кавычках на название в проекте)
        /// </summary>
        private int _pendingCount;
        /// <summary>
        /// Количество заказов со статусом "Завершено" (поменять в кавычках на название в проекте)
        /// </summary>
        private int _completedCount;

        /// <summary>
        /// Список DTO всех заказов
        /// </summary>
        public ObservableCollection<OrderItem> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }
        /// <summary>
        /// Количество заказов
        /// </summary>
        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }
        /// <summary>
        /// Количество заказов со статусом "В процессе" (поменять в кавычках на название в проекте)
        /// </summary>
        public int InProcessCount
        {
            get => _inProcessCount;
            set => SetProperty(ref _inProcessCount, value);
        }
        /// <summary>
        /// Количество заказов со статусом "Ожидают" (поменять в кавычках на название в проекте)
        /// </summary>
        public int PendingCount
        {
            get => _pendingCount;
            set => SetProperty(ref _pendingCount, value);
        }
        /// <summary>
        /// Количество заказов со статусом "Завершено" (поменять в кавычках на название в проекте)
        /// </summary>
        public int CompletedCount
        {
            get => _completedCount;
            set => SetProperty(ref _completedCount, value);
        }

        /// <summary>
        /// Команда загрузки заказов в таблицу
        /// </summary>
        public ICommand LoadOrdersCommand { get; }
        /// <summary>
        /// Команда открытия окна добавления нового заказа
        /// </summary>
        public ICommand AddOrderCommand { get; }


        public OrderListViewModel(OrderService orderService)
        {
            _orderService = orderService;
            Orders = new ObservableCollection<OrderItem>();

            LoadOrdersCommand = new RelayCommandAsync(
                execute: () => TryRunTaskAsync(LoadOrdersAsync, "Ошибка загрузки"),
                canExecute: () => !IsBusy
            );

            AddOrderCommand = new RelayCommand(OpenAddOrderWindow);

            LoadOrdersCommand.Execute(null);
        }


        /// <summary>
        /// Загрузка данных о заказах в список
        /// </summary>
        private async Task LoadOrdersAsync()
        {
            var orders = await _orderService.GetAllAsync();
            List<Order> activeOrders = await _orderService.GetActiveOrdersAsync();
            var items = new List<OrderItem>();

            foreach (var order in orders)
            {
                string clientName = order.Client?.Name ?? string.Empty;

                items.Add(new OrderItem
                {
                    Id = order.Id,
                    ClientName = clientName,
                    Route = $"{order.Address_From} → {order.Address_To}",
                    Status = order.Status ?? "—",
                    Price = order.Price,
                    OrderTime = order.Created_At.ToString()
                });
            }

            Orders.Clear();
            foreach (var item in items) Orders.Add(item);

            TotalCount = Orders.Count;

            // Изменить названия статусов на нужные проекту
            InProcessCount = Orders.Count(o => o.Status == "InProgress");
            PendingCount = Orders.Count(o => o.Status == "Pending");
            CompletedCount = Orders.Count(o => o.Status == "Done");
        }

        /// <summary>
        /// Открытие окна создания нового заказа
        /// <br/>Переделать на асинхронный!!!
        /// </summary>
        private void OpenAddOrderWindow()
        {
            var win = App.Services.GetRequiredService<NewOrderView>();
            if (win.ShowDialog() == true)
                LoadOrdersCommand.Execute(null);
        }
    }
}