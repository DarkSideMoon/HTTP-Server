using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WeatherLibrary;
using WeatherLibrary.Data;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.RouteFolder
{
    // Concrete realisation of Route
    public class WeatherRoute : Route
    {
        private Weather _weather;
        public WeatherRoute(string path, TcpClient client)
            : base(path, client)
        {
        }

        public WeatherRoute()
            : base()
        {
        }

        protected override void SendResponse()
        {
            _weather = new Weather(Route.Value); // value the city to get weather
            Tuple<WeatherCondition, WeatherWind, WeatherAtmosphere, WeatherAstronomy> resWeather = _weather.GetWeather();

            string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFolder = _path + "\\Website\\Styles\\SimplePageStyle.css";
            string css = string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(_pathToFolder))
            {
                css = reader.ReadToEnd();
            }

            string html = "<html>" +
                            "<head>" +
                                "<title>Weaher</title>" +
                                "<style type=\"text/css\">" + css + "</style>" +
                            "</head>" +
                                "<body>" +
                                    "<h2>" + this._weather.GetTitle() + "</h2>" +
                                    "<h3>Weather conditaion: </h3>" + "<hr>" +
                                        "<h4>Condition: </h4> <i>" + resWeather.Item1.Conditions + "</i>" +
                                        "<h4>Temperature: </h4> <i>" + resWeather.Item1.Temp + "</i>" +
                                        "<h4>Code: </h4> <i>" + resWeather.Item1.Code + "</i>" +
                                        "<h4>Date: </h4> <i>" + resWeather.Item1.Date + "</i>" +
                                    "<h3>Weather wind: </h3>" + "<hr>" +
                                        "<h4>Chill: </h4> <i>" + resWeather.Item2.Chill + "</i>" +
                                        "<h4>Direction: </h4> <i>" + resWeather.Item2.Direction + "</i>" +
                                        "<h4>Speed: </h4> <i>" + resWeather.Item2.Speed + "</i>" +
                                        "<h4>Date: </h4> <i>" + resWeather.Item1.Date + "</i>" +
                                    "<h3>Weather astronomy: </h3>" + "<hr>" +
                                        "<h4>Sunrise: </h4> <i>" + resWeather.Item4.Sunrise + "</i>" +
                                        "<h4>Sunset: </h4> <i>" + resWeather.Item4.Sunset + "</i>" +
                                 "</body>" +
                           "</html>";

            ResponseBuilder builder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    Html = html,
                    ContentLength = html.Length,
                    StatusDesc = ((HttpStatusCode)200).ToString(),
                    ContentType = "text/html",
                    StatusCode = 200,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };
            string str = builder.CreateResponse();

            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // Отправим его клиенту
            this.Client.GetStream().Write(buffer, 0, buffer.Length);
            // Закроем соединение
            this.Client.Close();
        }

        protected override void SendJson()
        {
            string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFile = _path + "\\Website\\Docs\\example.json";

            string json = string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(_pathToFile))
            {
                json = reader.ReadToEnd();
            }

            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            try
            {
                // Отправим его клиенту
                this.Client.GetStream().Write(buffer, 0, buffer.Length);
                // Закроем соединение
                this.Client.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
