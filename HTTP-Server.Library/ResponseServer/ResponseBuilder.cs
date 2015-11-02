using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ResponseServer
{
    public abstract class ResponseBuilder
    {
        protected Response response;
        protected StringBuilder stringBuilder;

        /// <summary>
        /// Get response instance
        /// </summary>
        public Response Response
        {
            get { return this.response; }
        }

        /// <summary>
        /// Get the all response in string
        /// </summary>
        public StringBuilder StringResult
        {
            get { return this.stringBuilder; }
        }

        // Abstract build methods
        public abstract void BuildStatus(int code); // HTTP/1.1 200 OK
        public abstract void BuildContent(string context); // Content-Length: 230 | Content-Type: text/html; charset=iso-8859-1
        public abstract void BuildTcpInfo(TcpClient client); // Connection: Closed | ProtocolType | AddressFamily
        public abstract void BuildInformation(); // Other information | Date
        public abstract void BuildHtml(); // Dived into to methods BuildHead and BuildBody
    }
}
