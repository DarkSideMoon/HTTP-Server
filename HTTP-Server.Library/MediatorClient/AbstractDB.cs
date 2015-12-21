using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.MediatorClient
{
    public enum TypeDB
    {
        SQLServer,
        MySQL,
        Oracle,
        PostqreSQL
    }

    public abstract class AbstractDB<T>
    {
        protected string _connectionString = string.Empty;
        protected TypeDB typeDb;
        protected MyClient _client;


        public AbstractDB(TypeDB typeDB)
        {
            this.typeDb = typeDB;

            this._connectionString = this.GetConnString();

            if (this._connectionString == string.Empty)
                throw new InvalidOperationException();
        }

        public abstract bool Auth(MyClient client);

        public abstract bool Create(string query);

        public abstract bool Update(string param);

        public abstract bool Delete(string param);

        public abstract T Read(string query);

        public abstract bool IsExsit(string param);

        private string GetConnString()
        {
            StringBuilder builder = new StringBuilder();
            switch (this.typeDb)
            {
                case TypeDB.SQLServer:
                    return builder.AppendLine("SQLServer connection string").ToString();
                case TypeDB.MySQL:
                    return builder.AppendLine("MySQL connection string").ToString();
                case TypeDB.Oracle:
                    return builder.AppendLine("Oracle connection string").ToString();
                case TypeDB.PostqreSQL:
                    return builder.AppendLine("PostqreSQL connection string").ToString();
                default:
                    break;
            }
            return string.Empty;
        }
    }
}
