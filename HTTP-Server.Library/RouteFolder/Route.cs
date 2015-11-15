using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.RouteFolder
{
    // Abstract class
    public class Route 
    {
        protected static Dictionary<int, string> dictionaryRotes = new Dictionary<int, string>()
        {
            { 1, "getWeather" },
            { 2, "getIp" }
        };

        public Route() { }

        public Route(string[] route, TcpClient client) // string[] route  1: path 2: value
        {
            try
            {
                Client = client;
                Value = route[2];
                this.Path = route[1];
                this.FindRoute();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        public static TcpClient Client { get; set; }
        public static string Value { get; set; }
        public string Path { get; set; }
        public bool IsRouting { get; set; }


        public void Send(string path)
        {
            if (path == "getIp")
                new IpRoute().SendResponse();
            if (path == "getWeather")
                new WeatherRoute().SendResponse();
        }

        protected virtual void SendResponse()
        {
        }

        private void FindRoute()
        {
            foreach (var item in dictionaryRotes)
                if (item.Value == this.Path)
                {
                    this.IsRouting = true;
                    break;
                }
                else
                    this.IsRouting = false;
        }
    }
}
