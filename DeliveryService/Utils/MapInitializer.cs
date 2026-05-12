using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Utils
{
    public static class MapInitializer
    {
        public static async Task Initialize(WebView2 MapWebView)
        {
            await MapWebView.EnsureCoreWebView2Async();

            string html = """
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="utf-8" />
            <script src="https://api-maps.yandex.ru/2.1/?apikey=ТВОЙ_API_KEY&lang=ru_RU"></script>
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
                ymaps.ready(function () {
                    var map = new ymaps.Map("map", {
                        center: [55.0415, 82.9346],
                        zoom: 12
                    });

                    var pointA = [55.0302, 82.9204];
                    var pointB = [55.0415, 82.9346];

                    var route = new ymaps.multiRouter.MultiRoute({
                        referencePoints: [pointA, pointB],
                        params: {
                            routingMode: "auto"
                        }
                    });

                    map.geoObjects.add(route);
                });
            </script>
        </body>
        </html>
        """;

            MapWebView.NavigateToString(html);
        }
    }
}
