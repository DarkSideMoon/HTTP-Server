using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.FactoryMethod
{
    public class ScheduleJsonStorage : JsonManager, IJsonStorage
    {
        public MyJson Download(string name)
        {
            throw new NotImplementedException();
        }

        public void Save(string name, MyJson docJson)
        {
            throw new NotImplementedException();
        }

        public MyJson Load(string name)
        {
            throw new NotImplementedException();
        }

        public override IJsonStorage CreateJsonStorage()
        {
            return new ScheduleJsonStorage();
        }
    }
}
