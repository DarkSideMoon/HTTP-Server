using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.RouteFolder
{
    public class Route
    {
        protected static Dictionary<int, string> dictionaryRotes = new Dictionary<int, string>() 
        {
            { 1, "getWeather" },
            { 2, "getIp" }
        };

        public string Value { get; set; }
        public string Path { get; set; }
        public bool IsRouting { get; set; }

        public Route(string[] route) // string[] route  1: path 2: value
        {
            try
            {
                this.Value = route[2];
                this.Path = route[1];
                this.FindRoute();
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
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

        protected virtual void SendResponse()
        {

        }
    }
}
