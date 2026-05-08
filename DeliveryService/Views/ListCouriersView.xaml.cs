using DeliveryService.Models;
using DeliveryService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeliveryService.Views
{
    /// <summary>
    /// Логика взаимодействия для ListCouriersView.xaml
    /// </summary>
    public partial class ListCouriersView : Window
    {
        public ListCouriersView(ListCouriersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
