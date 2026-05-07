using GMap.NET;
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

            // !!ХАРДКОД ОТСЮДА ПОТОМ УБРАТЬ в viewmodel
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            Map.MapProvider = GMapProviders.OpenCycleMap;
            Map.Position = new PointLatLng(55.0415, 82.9346); // Новосибирск
            Map.Zoom = 12;
            Map.MinZoom = 2;
            Map.MaxZoom = 18;
            Map.CanDragMap = true;
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            //!!
        }
    }
}
