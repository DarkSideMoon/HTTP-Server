using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpServer.Library.RouteFolder
{
    // Abstract class
    public class Route
    {
        protected static Dictionary<int, string> dictionaryRotes = new Dictionary<int, string>()
        {
            { 1, "getWeather" },
            { 2, "getIp" },
            { 3, "getJson" },
            { 4, "logIn" },
            { 5, "registration" }
        };

        public Route()
        {
        }

        public Route(string route, TcpClient client)
        {
            AllRoute = route;
            try
            {
                string[] newRoute = this.Parse(route);
                Client = client;
                Value = newRoute[2];
                this.Action = newRoute[1];

                this.FindRoute();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        public static string AllRoute { get; set; }
        public static TcpClient Client { get; set; }
        public static string Value { get; set; }
        public string Action { get; set; }
        public bool IsRouting { get; set; }


        public void Send(string path)
        {
            switch (path)
            {
                case "getIp":
                    new IpRoute().SendResponse();
                    break;
                case "getWeather":
                    new WeatherRoute().SendResponse();
                    break;
                case "getJson":
                    new WeatherRoute().SendJson();
                    break;
                case "logIn":
                    new LogInRoute().SendResponse();
                    break;
                case "registration":
                    new RegistrationRoute().SendResponse();
                    break;
                default:
                    return;
            }
        }

        protected virtual void SendResponse()
        {
        }

        protected virtual void SendJson()
        {
        }

        private void FindRoute()
        {
            foreach (var item in dictionaryRotes)
                if (item.Value == this.Action)
                {
                    this.IsRouting = true;
                    break;
                }
                else
                    this.IsRouting = false;
        }

        /// <summary>
        /// Parse the string by different parameters
        /// parsing url string like this => 127.0.0.1/{action}/{value}
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        private string[] Parse(string route)
        {
            return Regex.Split(route, @"/");
        }
    }
}
