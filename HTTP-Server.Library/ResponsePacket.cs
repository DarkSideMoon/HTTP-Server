using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace HttpServer.Library
{
    public class ResponsePacket
    {
        public ResponsePacket()
        {
            //Error = Server.ServerError.OK;
            this.StatusCode = HttpStatusCode.OK;
        }

        public string Redirect { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
        public Encoding Encoding { get; set; }
        //public Server.ServerError Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
