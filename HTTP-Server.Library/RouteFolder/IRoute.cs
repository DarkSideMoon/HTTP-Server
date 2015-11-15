using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.RouteFolder
{
    public interface IRoute
    {
        void SendResponse();
        Route LoadRoute();
    }
}
