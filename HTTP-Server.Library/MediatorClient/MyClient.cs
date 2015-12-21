using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public abstract class MyClient
    {
        public AbstractDB<object> AbstractDB { get; set; }

        public abstract void Operation();

        public virtual void SendJson(string param)
        {
            Console.WriteLine(param);
        }

        public virtual void SendXml(string param)
        {
            Console.WriteLine(param);
        }
    }
}
