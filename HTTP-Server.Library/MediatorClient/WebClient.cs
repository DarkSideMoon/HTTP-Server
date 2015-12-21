using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public class WebClient : MyClient
    {
        public override void Operation()
        {
            throw new NotImplementedException();
        }

        public override void SendJson(string param)
        {
            base.SendJson(param);
        }

        public override void SendXml(string param)
        {
            base.SendXml(param);
        }
    }
}
