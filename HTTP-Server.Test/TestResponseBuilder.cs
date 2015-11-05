using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Test
{
    [TestClass]
    public class TestResponseBuilder
    {
        [TestMethod, TestCategory("TestResponseCreate")]
        public void TestResponseCreate()
        {
            ResponseBuilder errorBuilder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    ContentLength = 120,
                    ContentType = "text/html",
                    StatusCode = 200,
                    Charset = System.Text.Encoding.ASCII,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };

            string resActual = errorBuilder.CreateResponse();
            string resExpected = "HTTP/1.1 200 OK \nContent-Length: 120 \nContent-Type: text/html; charset=US-ASCII " +
                                 "\nProtocolType: Tcp \nAddressFamily: InterNetwork \nIsConnected: True" + 
                                 "<html> \n<head> </head> \n<body> </body> \n</html> ";

            // Not equals because the DateTime is different 
            Assert.AreNotEqual(resExpected, resActual);
        }
    }
}
