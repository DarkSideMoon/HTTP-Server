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
        /// Property to get and set response instance
        /// </summary>
        public Response Response
        {
            get { return this.response; }
            set { this.response = value; }
        }

        /// <summary>
        /// Get the all response in string
        /// </summary>
        public StringBuilder ResponseString
        {
            get { return this.stringBuilder; }
        }

        public abstract string CreateResponse();

        public virtual void Clear()
        {
            this.response = null;
        }

        // Abstract build methods
        protected abstract void BuildStatus(); // HTTP/1.1 200 OK
        protected abstract void BuildContent(); // Content-Length: 230 | Content-Type: text/html; charset=iso-8859-1
        protected abstract void BuildTcpInfo(); // Connection: Closed | ProtocolType | AddressFamily
        protected abstract void BuildInformation(); // Other information | Date
        protected abstract void BuildHtml(); // Dived into to methods BuildHead and BuildBody
    }
}
