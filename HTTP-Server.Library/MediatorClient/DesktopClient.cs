using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public class DesktopClient : MyClient
    {
        public override void Operation()
        {
            throw new NotImplementedException();
        }

        public override void SendXml(string param)
        {
            base.SendXml(param);
        }
    }
}
