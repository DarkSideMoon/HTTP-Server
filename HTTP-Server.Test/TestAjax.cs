using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace HttpServer.Test
{
    [TestClass]
    public class TestAjax
    {
        [TestMethod, TestCategory("Ajax")]
        public void TestPOST()
        {
            string ajaxRequest = "POST / HTTP/1.1\r\nHost: 127.0.0.1\r\nConnection: keep-alive\r\nContent-Length: 47\r\n" +
                                 "Accept: application/json, text/javascript, */*; q=0.01\r\nOrigin: http://127.0.0.1\r\n" +
                                 "X-Requested-With:XMLHttpRequest\r\nUser-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 " +
                                 " (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36\r\n" +
                                 "Content-Type: application/json; charset=UTF-8\r\nReferer: http://127.0.0.1/logIn/\r\n " +
                                 "Accept-Encoding: gzip, deflate\r\n" +
                                 "Accept-Language: uk-UA,uk;q=0.8,ru;q=0.6,en-US;q=0.4,en;q=0.2\r\n\r\n" +
                                 "{ 'Email' : 'shark005@i.ua', 'Password' : '123456' }";

            string[] lines = ajaxRequest.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            try
            {
                // problem with string type "" => error
                var obj = JObject.Parse(lines[13]);
                string email = obj.Property("Email").Value.ToString();
                string pass = obj.Property("Password").Value.ToString();

                Assert.AreNotEqual(null, obj);
                Assert.AreEqual("shark005@i.ua", email);
                Assert.AreEqual("123456", pass);
            }
            catch (Newtonsoft.Json.JsonReaderException)
            {
                // exception in parsing json => the jsonString is not json!
            }
        }
    }
}
