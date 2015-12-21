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
            { 4, "registration" },
            { 5, "registrationFail" },
            { 6, "registrationTrue" },
            { 7, "logIn" },
            { 8, "logInFail" },
            { 9, "logInTrue" }
        };

        public Route()
        {
        }

        public Route(string route, TcpClient client)
        {
            StaticClient = client;
            this.AllRoute = route;
            try
            {
                string[] newRoute = this.Parse(route);
                this.Client = client;
                this.Action = newRoute[1];
                Value = newRoute[2];

                this.FindRoute();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }
        public static string Value { get; set; }
        public static TcpClient StaticClient { get; set; }
        public string AllRoute { get; set; }
        public TcpClient Client { get; set; }
        public string Action { get; set; }
        public bool IsRouting { get; set; }

        public void Send(string path)
        {
            switch (path)
            {
                case "getIp":
                    new IpRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "getWeather":
                    new WeatherRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "getJson":
                    new WeatherRoute(this.AllRoute, this.Client).SendJson();
                    break;

                case "logIn":
                    new LogInRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "logInFail":
                    new LogInRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "logInTrue":
                    new LogInRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "registration":
                    new RegistrationRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "registrationFail":
                    new RegistrationRoute(this.AllRoute, this.Client).SendResponse();
                    break;
                case "registrationTrue":
                    new RegistrationRoute(this.AllRoute, this.Client).SendResponse();
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
