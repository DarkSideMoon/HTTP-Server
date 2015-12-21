using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using HttpServer.Library.ClientLogic;
using HttpServer.Library.Other;
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
                this._client = client;
                this._user = new User();
                this._user.ParseUser(input);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Manage()
        {
            switch (this._user.TypeRequest)
            {
                case TypeJsonRequest.Registration:
                    {
                        this._route = !this._user.IsExist() ?
                            new Route("/registrationTrue/", this._client) : new Route("/registrationFail/", this._client);
                        this._route.Send(this._route.Action);
                    }
                    return;
                case TypeJsonRequest.LogIn:
                    {
                        this._route = this._user.IsExist() ?
                                new Route("/logInTrue/", this._client) : new Route("/logInFail/", this._client);
                        this._route.Send(this._route.Action);
                    }
                    return;
                default:
                    break;
            }
        }
    }
}
