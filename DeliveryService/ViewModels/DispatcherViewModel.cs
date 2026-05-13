using DeliveryService.Commands;
using DeliveryService.Models;
using DeliveryService.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace DeliveryService.ViewModels
{
    /// <summary>
    /// Логика взаимодействия пользователя и базы данных с DispatcherView
    /// </summary>
    public class DispatcherViewModel : BaseViewModel
    {
        /// <summary>
        /// Интервал таймера
        /// </summary>
        private const int TIMER_INTERVAL = 30; 

        /// <summary>
        /// Таймер, который перезагружает данные
        /// </summary>
        private DispatcherTimer _refreshTimer;
        /// <summary>
        /// Активен ли таймер
        /// </summary>
        private bool _isTimerActive = true;

        private readonly OrderService _orderService;
        private readonly CourierService _courierService;

        // ВАЖНО: Поменять названия статусов в комментариях
        /// <summary>
        /// Счётчик заказов со статусом "New"
        /// </summary>
        private int _newOrderCount;
        /// <summary>
        /// Счётчик заказов со статусом "InDelivery"
        /// </summary>
        private int _inTransitOrderCount;
        /// <summary>
        /// Счётчик заказов со статусом "Done"
        /// </summary>
        private int _completedOrderCount;
        /// <summary>
        /// Список активных заказов
        /// </summary>
        public ObservableCollection<Order> ActiveOrders { get; }
        /// <summary>
        /// Список курьеров, которые онлайн
        /// </summary>
        public ObservableCollection<Courier> OnlineCouriers { get; }

        // ВАЖНО: Поменять названия статусов в комментариях
        /// <summary>
        /// Счётчик заказов со статусом "New"
        /// </summary>
        public int NewOrderCount
        {
            get => _newOrderCount;
            set => SetProperty(ref _newOrderCount, value);
        }
        /// <summary>
        /// Счётчик заказов со статусом "InDelivery"
        /// </summary>
        public int InTransitOrderCount
        {
            get => _inTransitOrderCount;
            set => SetProperty(ref _inTransitOrderCount, value);
        }
        /// <summary>
        /// Счётчик заказов со статусом "Done"
        /// </summary>
        public int CompletedOrderCount
        {
            get => _completedOrderCount;
            set => SetProperty(ref _completedOrderCount, value);
        }

        /// <summary>
        /// Команда загрузки данных
        /// </summary>
        public ICommand LoadDataCommand { get; }


        public DispatcherViewModel(OrderService orderService, CourierService courierService)
        {
            _orderService = orderService;
            _courierService = courierService;

            ActiveOrders = new ObservableCollection<Order>();
            OnlineCouriers = new ObservableCollection<Courier>();

            LoadDataCommand = new RelayCommandAsync(
                execute: () => TryRunTaskAsync(LoadDataAsync, "Ошибка загрузки"),
                canExecute: () => !IsBusy
            );

            LoadDataCommand.Execute(null);

            InitializeTimer();
        }


        /// <summary>
        /// Загрузка данных о заказах
        /// </summary>
        private async Task LoadOrdersAsync()
        {
            var allOrders = await _orderService.GetAllAsync();

            if (allOrders != null && allOrders.Any())
            {
                var activeOrders = allOrders.Where(o => o.Status != "Done").OrderByDescending(o => o.Created_At).ToList();

                ActiveOrders.Clear();
                foreach (var order in activeOrders) 
                    ActiveOrders.Add(order);
                

                // Поменять статусы на нужные проекту
                
                NewOrderCount = activeOrders.Count(o => o.Status == "new");
                InTransitOrderCount = activeOrders.Count(o => o.Status == "InDelivery");
                CompletedOrderCount = allOrders.Count(o => o.Status == "Done");
            }
            else
            {
                ActiveOrders.Clear();

                NewOrderCount = 0;
                InTransitOrderCount = 0;
                CompletedOrderCount = 0;

                return;
            }
        }

        /// <summary>
        /// Загрузка данных об курьерах
        /// </summary>
        private async Task LoadCouriersAsync()
        {
            var allCouriers = await _courierService.GetAllAsync();
            if (allCouriers == null) 
                return;

            var onlineCouriers = allCouriers.Where(c => c.IsActive).ToList();

            OnlineCouriers.Clear();
            foreach (var courier in onlineCouriers)
                OnlineCouriers.Add(courier);
        }

        /// <summary>
        /// Загрузка данных
        /// </summary>
        private async Task LoadDataAsync()
        {
            await LoadOrdersAsync();
            await LoadCouriersAsync();
        }

        /// <summary>
        /// Логика таймера
        /// </summary>
        private void OnTimerTick(object? sender, EventArgs e)
        {
            bool windowExists = Application.Current.Windows
                .Cast<Window>()
                .Any(w => w.DataContext == this);

            if (!windowExists)
            {
                if (_isTimerActive)
                {
                    _refreshTimer.Stop();
                    _refreshTimer.Tick -= OnTimerTick;
                    _isTimerActive = false;
                }
                return;
            }

            Debug.WriteLine("Done");
            LoadDataCommand.Execute(null);
        }

        /// <summary>
        /// Инициализация таймера
        /// </summary>
        private void InitializeTimer()
        {
            _refreshTimer = new DispatcherTimer();
            _refreshTimer.Interval = TimeSpan.FromSeconds(TIMER_INTERVAL);
            _refreshTimer.Tick += OnTimerTick;
            _refreshTimer.Start();
        }
    }
}