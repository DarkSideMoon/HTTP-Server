using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library
{
    public class Route
    {
        public string Verb { get; set; }
        public string Path { get; set; }

        //public RouteHandler Handler { get; set; }
    }
}
