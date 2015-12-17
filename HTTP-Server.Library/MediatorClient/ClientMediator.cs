using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.StateServer;

namespace HttpServer.Library.MediatorClient
{
    public class ClientMediator : Mediator
    {
        public HttpError Error { get; set; }
        public HttpListen Listen { get; set; }
        public HttpEstablished Established { get; set; }
        public override System.Net.Sockets.TcpClient Client { get; set; }

        public override void Send(int code, State state)
        {
            // Get null of state and check only by class
            if (state.GetType() == typeof(HttpError))
                state.SendResponse(code);
            else if (state.GetType() == typeof(HttpListen))
                state.SendResponse();
            else
                state.SendResponse();
        }
    }
}
