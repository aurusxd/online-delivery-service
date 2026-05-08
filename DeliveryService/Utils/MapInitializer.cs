using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Utils
{
    public static class MapInitializer
    {
        public static void Initialize(GMapControl map, double lat = 55.0415, double lon = 82.9346)
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            map.MapProvider = GMapProviders.OpenCycleMap;
            map.Position = new PointLatLng(lat, lon);
            map.Zoom = 12;
            map.MinZoom = 2;
            map.MaxZoom = 18;
            map.CanDragMap = true;
            map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            map.ShowCenter = false;
        }
    }
}
