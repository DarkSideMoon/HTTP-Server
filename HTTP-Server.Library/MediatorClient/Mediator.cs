using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public abstract class Mediator
    {
        public abstract System.Net.Sockets.TcpClient Client { get; set; } // the second step is initialize TcpClient
        public abstract void Send(int code, State state);
    }
}
