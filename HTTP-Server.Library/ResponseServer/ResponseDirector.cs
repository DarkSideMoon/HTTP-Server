using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ResponseServer
{
    public class ResponseDirector
    {
        public ResponseDirector(ResponseBuilder builder, TcpClient client, int code, string context)
        {
            // Build the main response information
            builder.BuildStatus(code);
            builder.BuildInformation();
            builder.BuildContent(context);
            builder.BuildTcpInfo(client);

            // Build the html page
            builder.BuildHtml();
        }
    }
}
