using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ResponseServer
{
    public class JsonBuilder : ResponseBuilder
    {
        public JsonBuilder()
        {
            this.response = new Response();
            this.stringBuilder = new StringBuilder();
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

        protected override void BuildStatus()
        {
            throw new NotImplementedException();
        }

        protected override void BuildContent()
        {
            throw new NotImplementedException();
        }

        protected override void BuildTcpInfo()
        {
            throw new NotImplementedException();
        }

        protected override void BuildInformation()
        {
            throw new NotImplementedException();
        }

        protected override void BuildHtml()
        {
            throw new NotImplementedException();
        }
    }
}
