using DeliveryService.Utils;
using System.Windows;


namespace DeliveryService.Views
{
    /// <summary>
    /// Логика взаимодействия для DispatcherView.xaml
    /// </summary>
    public partial class DispatcherView : Window
    {
        public DispatcherView()
        {
            InitializeComponent();
            MapInitializer.Initialize(Map);

        }
        
    
    }
}
