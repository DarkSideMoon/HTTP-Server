using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ResponseServer
{
    public class PageBuilder : ResponseBuilder
    {
        public PageBuilder()
        {
            this.response = new Response();
            this.stringBuilder = new StringBuilder();
        }

        protected override void BuildStatus()
        {
            string codeStr = this.response.StatusCode.ToString() + " " + ((HttpStatusCode)this.response.StatusCode).ToString();
            string resStr = "HTTP/1.1 " + codeStr;

            this.stringBuilder.Append(resStr).AppendLine();
        }

        protected override void BuildContent()
        {
            this.stringBuilder.Append("Content-Length: " + this.response.ContentLength).AppendLine();
            this.stringBuilder.Append("Content-Type: text/html; charset=" + this.response.Charset.EncodingName).AppendLine();
        }

        protected override void BuildTcpInfo()
        {
            this.stringBuilder.Append("ProtocolType: " + this.response.ProtocolType).AppendLine();
            this.stringBuilder.Append("AddressFamily: " + this.response.AddressFamily).AppendLine();
            this.stringBuilder.Append("IsConnected: " + this.response.IsConnected).AppendLine();
        }

        protected override void BuildInformation()
        {
            this.response.DateTimeResponse = DateTime.Now;

            this.stringBuilder.Append("Date: " + DateTime.Now).AppendLine();
        }

        protected override void BuildHtml()
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

        public override string CreateResponse()
        {
            this.BuildStatus();
            this.BuildContent();
            this.BuildTcpInfo();
            this.BuildInformation();
            this.BuildHtml();

            return this.stringBuilder.ToString();
        }
    }
}
