﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.IpConfig;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.RouteFolder
{
    // Concrete realisation of Route
    public class IpRoute : Route
    {
        private IpInfoData _dataIp;
        public IpRoute(string path, TcpClient client)
            : base(path, client)
        {
        }

        public IpRoute()
            : base()
        {
        }

        protected override void SendResponse()
        {
            string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFolder = _path + "\\Website\\Styles\\SimplePageStyle.css";
            string css = string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(_pathToFolder))
            {
                css = reader.ReadToEnd();
            }

            _dataIp = IpConfig.IpConfig.GetIpInfo();
            string html = "<html>" +
                            "<head>" +
                                "<title>Weaher</title>" +
                                "<style type=\"text/css\">" + css + "</style>" +
                            "</head>" +
                                "<body>" +
                                    "<h1>Ip information</h1>" + "<hr>" +
                                    "<h3>IP: </h3>" + "<i>" + this._dataIp.Ip + "</i>" +
                                    "<h3>City: </h3>" + "<i>" + this._dataIp.City + "</i>" +
                                    "<h3>Country: </h3>" + "<i>" + this._dataIp.Country + "</i>" +
                                    "<h3>Region: </h3>" + "<i>" + this._dataIp.Region + "</i>" +
                                    "<h3>Host name: </h3>" + "<i>" + this._dataIp.HostName + "</i>" +
                                    "<h3>Organization: </h3>" + "<i>" + this._dataIp.Organization + "</i>" +
                                    "<h3>Location: </h3>" + "<i>" + this._dataIp.Location + "</i>" +
                                 "</body>" +
                           "</html>";

            ResponseBuilder builder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    Html = html,
                    ContentLength = html.Length,
                    StatusDesc = ((HttpStatusCode)200).ToString(),
                    ContentType = "text/html",
                    StatusCode = 200,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };
            string str = builder.CreateResponse();

            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // Отправим его клиенту
            this.Client.GetStream().Write(buffer, 0, buffer.Length);
            // Закроем соединение
            this.Client.Close();
        }
    }
}
