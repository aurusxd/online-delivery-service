using DeliveryService.Commands;
using DeliveryService.Models;
using DeliveryService.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DeliveryService.ViewModels
{
    public class DispatcherViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;
        private readonly CourierService _courierService;

        private int _newOrderCount;
        private int _inTransitOrderCount;
        private int _completedOrderCount;

        public ObservableCollection<Order> ActiveOrders { get; }
        public ObservableCollection<Courier> OnlineCouriers { get; }

        public int NewOrderCount
        {
            get => _newOrderCount;
            set => SetProperty(ref _newOrderCount, value);
        }

        public int InTransitOrderCount
        {
            get => _inTransitOrderCount;
            set => SetProperty(ref _inTransitOrderCount, value);
        }

        public int CompletedOrderCount
        {
            get => _completedOrderCount;
            set => SetProperty(ref _completedOrderCount, value);
        }

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
        }


        private async Task LoadOrdersAsync()
        {
            var allOrders = await _orderService.GetAllAsync();
            var activeOrders = allOrders.Where(o => o.Status != "Done").OrderByDescending(o => o.Created_At).ToList();

            ActiveOrders.Clear();
            foreach (var order in activeOrders)
                ActiveOrders.Add(order);

            // Поменять статусы на нужные проекту
            NewOrderCount = activeOrders.Count(o => o.Status == "New");
            InTransitOrderCount = activeOrders.Count(o => o.Status == "InDelivery");
            CompletedOrderCount = allOrders.Count(o => o.Status == "Done");
        }

        private async Task LoadCouriersAsync()
        {
            var allCouriers = await _courierService.GetAllAsync();
            var onlineCouriers = allCouriers.Where(c => c.IsActive).ToList();

            OnlineCouriers.Clear();
            foreach (var courier in onlineCouriers)
                OnlineCouriers.Add(courier);
        }

        private async Task LoadDataAsync()
        {
            await LoadOrdersAsync();
            await LoadCouriersAsync();
        }
    }
}