using System;
using System.Net.Sockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library.IpConfig;
using HttpServer.Library.RouteFolder;

namespace HttpServer.Test
{
    [TestClass]
    public class TestIp
    {
        [TestMethod]
        public void TestIpInof()
        {
            //IpInfoData data = IpConfig.GetIpInfo();
            //string resExpected = "78.27.148.159";
            // appveyor ip config
            // Assert.AreEqual failed. Expected:<78.27.148.159>. Actual:<104.197.110.30>.
            //Assert.AreEqual(resExpected, data.Ip);
        }
    }
}
