using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.StateServer
{
    public class HttpEstablished : State
    {
        public HttpEstablished(Server server)
        {
            this.Server = server;
        }

        protected override void GetResponse()
        {
            ResponseBuilder pageBuilder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    ContentLength = 120,
                    StatusDesc = this.Server.State.DescriptionState,
                    ContentType = "text/html",
                    StatusCode = this.Server.State.CodeState,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };
            this.Response = pageBuilder.CreateResponse();
        }

        protected override void ChangeState(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
