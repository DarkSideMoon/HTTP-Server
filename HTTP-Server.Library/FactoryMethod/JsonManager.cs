using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.FactoryMethod
{
    public abstract class JsonManager
    {
        public MyJson MyJson
        {
            get;
            set;
        }

        public abstract IJsonStorage CreateJsonStorage();

        public bool Save(MyJson json)
        {
            if (json.IsSave)
                return false;
            //Fucktory
            // using Factory method to create new storage
            IJsonStorage storage = this.CreateJsonStorage();
            storage.Save(json.Name, json);
            return true;
        }
    }
}
