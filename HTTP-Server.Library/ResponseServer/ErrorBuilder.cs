using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ResponseServer
{
    public class ErrorBuilder : ResponseBuilder
    {
        public ErrorBuilder()
        {
            this.response = new Response();
            this.stringBuilder = new StringBuilder();
        }

        public override void BuildStatus(int code)
        {
            this.response.StatusCode = code;
            this.response.StatusDesc = ((HttpStatusCode)code).ToString();

            string codeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            string resStr = "HTTP/1.1 " + codeStr;

            this.stringBuilder.Append(resStr).AppendLine();
        }

        public override void BuildContent(string context)
        {
            this.response.ContentType = "text/html";
            this.response.ContentLength = context.Length;

            this.stringBuilder.Append("Content-Length: " + context.Length).AppendLine();
            this.stringBuilder.Append("Content-Type: text/html; charset=" + context.Length).AppendLine();
        }

        public override void BuildTcpInfo(TcpClient client)
        {
            this.response.ProtocolType = client.Client.ProtocolType;
            this.response.AddressFamily = client.Client.AddressFamily;
            this.response.IsConnected = client.Client.Connected;

            this.stringBuilder.Append("ProtocolType: " + client.Client.ProtocolType).AppendLine();
            this.stringBuilder.Append("AddressFamily: " + client.Client.AddressFamily).AppendLine();
            this.stringBuilder.Append("IsConnected: " + client.Client.Connected).AppendLine();
        }

        public override void BuildInformation()
        {
            this.response.DateTimeResponse = DateTime.Now;

            this.stringBuilder.Append("Date: " + DateTime.Now).AppendLine();
        }

        public override void BuildHtml()
        {
            this.stringBuilder.Append("<html>").AppendLine();

            this.stringBuilder.Append(this.BuildHead()).AppendLine();
            this.stringBuilder.Append(this.BuildBody()).AppendLine();

            this.stringBuilder.Append("</html>").AppendLine();
        }

        private string BuildHead()
        {
            string head = "<head> " + "</head>";
            return head;
        }

        private string BuildBody()
        {
            string body = "<body> " + "</body>";
            return body;
        }
    }
}
