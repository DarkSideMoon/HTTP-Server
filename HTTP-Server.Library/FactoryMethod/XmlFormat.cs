using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.FactoryMethod
{
    public class XmlFormat : Format
    {
        public override void Create(string name, Format myFormat)
        {
            ResponseBuilder builder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    Html = myFormat.Content,
                    ContentLength = myFormat.Length,
                    StatusDesc = ((HttpStatusCode)200).ToString(),
                    ContentType = "text/html",
                    StatusCode = 200,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };
            string str = builder.CreateResponse();
        }

        public override string GetInfoFormat()
        {
            return string.Format("Status code: 200 \nContentType = text/html\nTime now: {0}", DateTime.Now);
        }
    }
}
