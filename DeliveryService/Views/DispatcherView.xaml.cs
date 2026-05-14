using DeliveryService.Models;
using DeliveryService.Utils;
using DeliveryService.ViewModels;
using Microsoft.Web.WebView2.Core;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;


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
            DataContext = viewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await MapInitializer.Initialize(Map);
  
            if (DataContext is DispatcherViewModel vm)
            {
                vm.OrderSelected += async order =>
                {
                    await Map.CoreWebView2.ExecuteScriptAsync(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "DrawRoute({0}, {1}, {2}, {3})",
                            order.Lat_From, order.Lon_From, order.Lat_To, order.Lon_To));
                };

                vm.CourierSelected += async (Lat_From,Lan_From,Lat_To,Lan_To) =>
                {
                    await Map.CoreWebView2.ExecuteScriptAsync(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            "DrawRoute({0}, {1}, {2}, {3})",
                            Lat_From, Lan_From, Lat_To, Lan_To));

                    //await Map.CoreWebView2.ExecuteScriptAsync(
                    //string.Format(
                    //    CultureInfo.InvariantCulture,
                    //    "AddMark({0}, {1})",
                    //    courier.Current_Lat,courier.Current_Lon));
                };
            }
        }
    }
}
