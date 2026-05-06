using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DeliveryService.Models;

namespace DeliveryService.ViewModels
{
    public class CreateOrderViewModel : BaseViewModel
    {
        private string _addressFrom = string.Empty;
        private string _addressTo = string.Empty;
        private decimal _price;

        public string AddressFrom
        {
            get => _addressFrom;
            set { _addressFrom = value; OnPropertyChanged(); }
        }

        public string AddressTo
        {
            get => _addressTo;
            set { _addressTo = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public ICommand ConfirmOrderCommand { get; }

        public CreateOrderViewModel()
        {
            ConfirmOrderCommand = new RelayCommand(o => CreateOrder());
            Price = 500;
        }

        private void CreateOrder()
        {
            if (string.IsNullOrWhiteSpace(AddressFrom) || string.IsNullOrWhiteSpace(AddressTo))
            {
                System.Windows.MessageBox.Show("Укажите оба адреса для оформления доставки.");
                return;
            }

            System.Windows.MessageBox.Show($"Заказ успешно оформлен!\nМаршрут: {AddressFrom} — {AddressTo}\nК оплате: {Price} ₽");
        }

        public class RelayCommand : ICommand
        {
            private readonly Action<object?> _execute;
            public RelayCommand(Action<object?> execute) => _execute = execute;

            public bool CanExecute(object? parameter) => true;
            public void Execute(object? parameter) => _execute(parameter);

            public event EventHandler? CanExecuteChanged { add { } remove { } }
        }
    }
}
        
