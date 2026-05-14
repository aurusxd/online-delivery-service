
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


                    function DrawRoute(startLat,startLon,endLat,endLon)
                    {
                        map.geoObjects.removeAll();
                       console.log("DrawRoute вызван:", startLat, startLon, endLat, endLon);
                        ymaps.route([
                            [startLat, startLon],
                            [endLat, endLon],
                        ]).then(function(route) {
                            console.log("Маршрут построен, добавляем на карту");
                            map.geoObjects.add(route);
                        }).catch(function(err) {
                            console.log("Ошибка:", err);
                        });
                    }
                    function AddMark(Lat,Lon)
                    {

            
                        console.log("AddMark вызван:", Lat,Lon);
                        var marker = new ymaps.Placemark(
                        [lat, lon],
                        {
                            balloonContent: 'Курьер'
                        },
                        {
                            preset: 'islands#blueDeliveryIcon',
                            zIndex: 9999,
                            iconOffset: [0, -20]
                        }
                    );

                    map.geoObjects.add(marker);
                    }

                </script>
            </body>
            </html>
            """;
            await MapWebView.EnsureCoreWebView2Async();
            MapWebView.CoreWebView2.WebMessageReceived += (sender, args) =>
            {
                string json = args.WebMessageAsJson;

                var list = JsonSerializer.Deserialize<AddressDTO>(json);
                AddressSelected?.Invoke(list.lat, list.lon, list.address);
            };

        MapWebView.NavigateToString(html);
        }
    }
}
