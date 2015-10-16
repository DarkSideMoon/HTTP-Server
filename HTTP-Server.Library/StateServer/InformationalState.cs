using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.StateServer
{
    public enum InformationalType
    {
        Continue = 100,
        Processing = 102
    }

    public class InformationalState : State
    {
        public InformationalState(State state)
        {
            this._server = state.Server; // the state of server 
            this._codeState = state.CodeState; // the state of code // in depend on code state set the description and response messsage in browser
        }

        private void Initalize()
        {
            
        }
    }
}
