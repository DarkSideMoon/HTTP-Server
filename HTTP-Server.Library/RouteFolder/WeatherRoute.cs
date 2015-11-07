using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.RouteFolder
{
    public class WeatherRoute : Route
    {
        public WeatherRoute(string[] route)
            : base(route)
        {

        }

        protected override void SendResponse()
        {
            base.SendResponse();
        }
    }
}
