
using DeliveryService.DTO;
using Microsoft.Web.WebView2.Wpf;
using System.Net;
using System.Text.Json;
using System.Windows;
using System.Windows.Interop;

namespace DeliveryService.Utils
{
    public static class MapInitializer
    {
        /// <summary>
        /// Событие при выборе адреса на карте
        /// </summary>
        public static event Action<double, double, string>? AddressSelected;
        public static event Func<List<List<double>>, Task>? CoordinatesRoute;

        public static async Task Initialize(WebView2 MapWebView)
        {


            string html = """ 
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8" />
                <script src="https://api-maps.yandex.ru/2.1/?apikey=7884a1f6-e701-4ae1-bca9-d35b02adaf1e&lang=ru_RU"></script>
                <style>
                    html, body, #map {
                        width: 100%;
                        height: 100%;
                        margin: 0;
                        padding: 0;
                    }
                </style>
            </head>
            <body>
                <div id="map"></div>

                <script>
                    var map;
                    var courierMark = null;
                    const routes = []

                    ymaps.ready(function () {
                        map = new ymaps.Map("map", {
                            center: [55.0415, 82.9346],
                            zoom: 12
                        });

                        map.events.add('click', function (e) {
                            var coords = e.get('coords');

                            ymaps.geocode(coords).then(function (res) {
                                var firstGeoObject = res.geoObjects.get(0);
                                var address = firstGeoObject.getAddressLine();

                                window.chrome.webview.postMessage({
                                    type: "mapClick",
                                    lat: coords[0],
                                    lon: coords[1],
                                    address: address
                                });
                            });
                        });
                    });

                    function clearObjects()
                    {
                        map.geoObjects.removeAll(); 
                    }

                    function DrawRoute(startLat,startLon,endLat,endLon)
                    { 
                       console.log("DrawRoute вызван:", startLat, startLon, endLat, endLon);
                        ymaps.route([
                            [startLat, startLon],
                            [endLat, endLon],
                        ]).then(function(route) {
                            console.log("Маршрут построен, добавляем на карту");

                            map.geoObjects.add(route);
                            routes.push(route);

                            var paths = route.getPaths();

                            paths.each(function(path) {

                                var coordinates = path.geometry.getCoordinates();
                                console.log(coordinates);
                                window.routeCoordinates = coordinates;
                                 window.chrome.webview.postMessage({
                                    type: "routeCoordinates",
                                    coordinates: coordinates
                                });

                            });

    
                        }).catch(function(err) {
                            console.log("Ошибка:", err);
                        });
                    }
                    function AddMark(lat,lon)
                    {

                    courierMarker = new ymaps.Placemark(
                        [lat, lon],
                        {
                            balloonContent: 'Курьер'
                        },
                        {
                            iconLayout: 'default#imageWithContent',
                            iconImageHref: 'https://cdn-icons-png.flaticon.com/512/684/684908.png',
                            iconImageSize: [36, 36],
                            iconImageOffset: [-18, -36],
                            iconContentLayout: ymaps.templateLayoutFactory.createClass(
                                '<div style="color:white;background:#2563EB;border-radius:50%;width:22px;height:22px;text-align:center;line-height:22px;font-weight:bold;border:2px solid white;">К</div>'
                            ),
                            zIndex: 9999,
                            zIndexActive: 10000
                        }
                    );

                    map.geoObjects.add(courierMarker);
                    }


                   function MoveCourier(lat, lon) {

                        if (courierMarker != null) {
                            courierMarker.geometry.setCoordinates([lat, lon]);
                        }
                    }

                </script>
            </body>
            </html>
            """;
            await MapWebView.EnsureCoreWebView2Async();
            MapWebView.CoreWebView2.WebMessageReceived += (sender, args) =>
            {
                string json = args.WebMessageAsJson;
                var routeData = JsonSerializer.Deserialize<CoordinatesDTO>(json);
               // var list = JsonSerializer.Deserialize<AddressDTO>(json);
                CoordinatesRoute?.Invoke(routeData.coordinates);
                //AddressSelected?.Invoke(list.lat, list.lon, list.address);
            };

        MapWebView.NavigateToString(html);
        }
    }
}
