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
using System.Collections.ObjectModel;
using DeliveryService.Models;

namespace DeliveryService.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
   
        public partial class MenuView : Window
        {
           
            public ObservableCollection<Product> MenuItems { get; set; }

            public MenuView()
            {
                InitializeComponent();

                MenuItems = new ObservableCollection<Product>
            {
                new Product { Name = "Плов с курицей", Weight = "250 г", Price = "249 руб", ImagePath = "/DeliveryService;component/Images/plov.png" },
                new Product { Name = "Круассан", Weight = "60 г", Price = "89 руб", ImagePath = "/DeliveryService;component/Images/croissant.png" },
                new Product { Name = "Капучино", Weight = "300 мл", Price = "149 руб", ImagePath = "/DeliveryService;component/Images/coffee.png" },
                new Product { Name = "Чизкейк", Weight = "100 г", Price = "179 руб", ImagePath = "/DeliveryService;component/Images/cheesecake.png" }
            };

                DataContext = this;
            }
        }
    }