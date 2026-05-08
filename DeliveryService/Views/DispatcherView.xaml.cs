<<<<<<< HEAD
﻿using DeliveryService.Utils;
using GMap.NET;
=======
﻿using GMap.NET;
>>>>>>> 8878fab27af73cdd7eca8b6682f01abf3c572c0a
using GMap.NET.MapProviders;
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
