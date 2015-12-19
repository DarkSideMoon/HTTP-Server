using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.ClientLogic;
using HttpServer.Library.Other;
using System.Net.Sockets;
using HttpServer.Library.RouteFolder;

namespace HttpServer.Library.JsonParsing
{
    public class JsonManager
    {
        private User _user;
        private TcpClient _client;
        private Route _route;

        /// <summary>
        /// Constructor to detect type of json request 
        /// </summary>
        /// <param name="input">Input json</param>
        public JsonManager(string input, TcpClient client)
        {
            try
            {
                _client = client;
                _user = new User(); 
                _user.ParseUser(input);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void Manage()
        {
            switch (_user.TypeRequest)
            {
                case TypeJsonRequest.Registration:
                    {
                        _route = _user.IsExist() ?
                            new Route("/registrationFail/", this._client) : new Route("/registrationFail/", this._client);
                        _route.Send(_route.Action);
                    }
                    break;
                case TypeJsonRequest.LogIn:
                    {
                        _route = _user.IsExist() ?
                                new Route("/logInTrue/", this._client) : new Route("/logInFail/", this._client);
                        _route.Send(_route.Action);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
