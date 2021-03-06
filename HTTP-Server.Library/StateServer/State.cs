﻿using System;
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

        public State()
        {
        }


        public Server MyServer
        {
            get { return this._server; }
            set { this._server = value; }
        }

        public int CodeState
        {
            get { return this._codeState; }
            set { this._codeState = value; }
        }

        public string DescriptionState
        {
            get { return this._descState; }
            set { this._descState = value; }
        }

        public string Response
        {
            get { return this._response; }
            set { this._response = value; }
        }

        /// <summary>
        /// Return the response of the state of server
        /// </summary>
        public abstract void SendResponse();

        /// <summary>
        /// Return the response of the state of server
        /// With error code
        /// </summary>
        public abstract void SendResponse(int code);

        internal virtual void HandleStateServer(Server server)
        {
            this.ChangeState(server);
        }

        protected abstract void ChangeState(Server server);
    }
}
