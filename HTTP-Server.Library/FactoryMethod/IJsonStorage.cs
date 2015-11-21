using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.FactoryMethod
{
    public interface IJsonStorage
    {
        MyJson Download(string name);
        void Save(string name, MyJson docJson);
        MyJson Load(string name);
    }
}
