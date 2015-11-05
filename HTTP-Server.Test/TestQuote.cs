using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library;

namespace HttpServer.Test
{
    [TestClass]
    public class TestQuote
    {
        [TestMethod]
        public void TestQuotes()
        {
            Quote q = new Quote();
            string res = q.GetQuote();
        }
    }
}
