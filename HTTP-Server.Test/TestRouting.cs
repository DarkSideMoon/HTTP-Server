using System;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library.RouteFolder;

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
