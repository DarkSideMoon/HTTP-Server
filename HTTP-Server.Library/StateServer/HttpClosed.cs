using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.StateServer
{
    public class HttpClosed : State
    {
        public HttpClosed(Server server)
        {
            this.Server = server;
        }

        protected override void GetResponse()
        {
            throw new NotImplementedException();
        }

        protected override void ChangeState(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
