using HttpServer.Library.StateServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library
{
    public abstract class State
    {
        protected internal Server _server;

        protected internal int _codeState; // the code of http server is stated in
        protected internal string _descState; // description of the state http server
        protected internal string _response;

        public Server Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public int CodeState
        {
            get { return _codeState; }
            set { _codeState = value; }
        }

        public string DescriptionState
        {
            get { return _descState; }
            set { _descState = value; }
        }

        public string Response
        {
            get { return _response; }
            set { _response = value; }
        }
    }
}
