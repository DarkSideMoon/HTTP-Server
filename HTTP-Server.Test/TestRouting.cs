using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library.RouteFolder;
using System.Net.Sockets;

namespace HttpServer.Test
{
    [TestClass]
    public class TestRouting
    {
        [TestMethod]
        public void TestRoute()
        {
            string action = "/logInTrue/";
            Route route  = new Route(action, new TcpClient());

            Assert.AreEqual(route.Action, "logInTrue");
        }
    }
}
