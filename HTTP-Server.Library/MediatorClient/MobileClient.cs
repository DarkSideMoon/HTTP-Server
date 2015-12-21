using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public class MobileClient : MyClient
    {
        public override void Operation()
        {
            throw new NotImplementedException();
        }

        public override void SendJson(string param)
        {
            base.SendJson(param);
        }
    }
}
