using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.StateServer
{
    public class HttpListen : State
    {
        public override void SendResponse()
        {
            ResponseBuilder pageBuilder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    ContentLength = 120,
                    StatusDesc = this.MyServer.State.DescriptionState,
                    ContentType = "text/html",
                    StatusCode = this.MyServer.State.CodeState,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };
            this.Response = pageBuilder.CreateResponse();

            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.UTF8.GetBytes(this.Response);
            // Отправим его клиенту
            //this._mediator.Client.GetStream().Write(buffer, 0, buffer.Length);
            // Закроем соединение
            //this._mediator.Client.Close();
        }

        public override void SendResponse(int code)
        {
            throw new NotImplementedException();
        }

        protected override void ChangeState(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
