using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library.ClientLogic;

namespace HttpServer.Test
{
    [TestClass]
    public class TestToken
    {
        [TestMethod]
        public void TestMyToken()
        {
            Token token = new Token();
            
            //System.Threading.Thread.Sleep(new TimeSpan(0, 1, 30));



            Assert.AreEqual(null, token);
        }
    }
}
