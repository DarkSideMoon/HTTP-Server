using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.StateServer
{
    public class HttpError : State
    {
        public HttpError()
            : base()
        {
        }


        public override void SendResponse(int code)
        {
            string codeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            Quote q = new Quote();

            string html = "<html>" +
                                "<body>" +
                                    "<h1>" + codeStr + "</h1>" +
                                    "<h3>Quote of the day</h3>" +
                                    "<blockquote><h3><i>" + q.GetQuote() + "</i></h3></blockquote>" +
                                    "<h3>Черномырдин Виктор Степанович</h3>" +
                                 "</body>" +
                           "</html>";

            ResponseBuilder builder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    Html = html,
                    ContentLength = html.Length,
                    StatusDesc = ((HttpStatusCode)code).ToString(),
                    ContentType = "text/html",
                    StatusCode = code,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };

            string str = builder.CreateResponse();

            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // Отправим его клиенту
            // this.Client.GetStream().Write(buffer, 0, buffer.Length);
            // Закроем соединение
            // this.Client.Close();
        }

        public override void SendResponse()
        {
            throw new NotImplementedException();
        }

        protected override void ChangeState(Server server)
        {
            throw new NotImplementedException();
        }
    }
}
