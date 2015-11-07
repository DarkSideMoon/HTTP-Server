using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HttpServer.Library.IpConfig
{
    public class IpConfig
    {
        private static JObject _dataObject;
        private static string _result = string.Empty;
        private static string _url = "http://ipinfo.io/json";

        public static IpInfoData GetIpInfo()
        {
            IpInfoData dataIp = new IpInfoData();

            using(WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                _result = wc.DownloadString(_url);
            }

            _dataObject = JObject.Parse(_result);
            try
            {
                dataIp = JsonConvert.DeserializeObject<IpInfoData>(_dataObject.ToString());
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataIp;
        }
    }
}
