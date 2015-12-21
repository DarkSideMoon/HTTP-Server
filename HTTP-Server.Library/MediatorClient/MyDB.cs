using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public class MyDB<T> : AbstractDB<T>
    {
        public MyDB(TypeDB typeDB)
            : base(typeDB)
        {
        }
        public DesktopClient DesktopClient { get; set; }

        public WebClient WebClient { get; set; }

        public MobileClient MobileClient { get; set; }

        public override bool Auth(MyClient client)
        {
            throw new NotImplementedException();
        }

        public override bool Create(string query)
        {
            throw new NotImplementedException();
        }

        public override bool Update(string param)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(string param)
        {
            throw new NotImplementedException();
        }

        public override T Read(string query)
        {
            throw new NotImplementedException();
        }

        public override bool IsExsit(string param)
        {
            throw new NotImplementedException();
        }
    }
}
