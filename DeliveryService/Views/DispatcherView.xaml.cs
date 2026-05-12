using DeliveryService.Utils;
using DeliveryService.ViewModels;
using System.Windows;


namespace DeliveryService.Views
{
    /// <summary>
    /// Логика взаимодействия для DispatcherView.xaml
    /// </summary>
    public partial class DispatcherView : Window
    {
        public DispatcherView(DispatcherViewModel viewModel)
        {
            InitializeComponent();
            MapInitializer.Initialize(Map);
            DataContext = viewModel;
        }
        
    
    }
}
