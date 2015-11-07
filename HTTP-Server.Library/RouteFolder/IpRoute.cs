using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.RouteFolder
{
    public class IpRoute : Route
    {
        public IpRoute(string[] route) 
            : base(route)
        {

        }

        protected override void SendResponse()
        {
            base.SendResponse();
        }
    }
}
