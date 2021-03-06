﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library.RouteFolder
{
    public class MainPageRoute : Route
    {
        public MainPageRoute(string path, TcpClient client)
            : base(path, client)
        {
        }

        public MainPageRoute()
            : base()
        {
        }

        protected override void SendResponse()
        {
            string _path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFolder = _path + "\\Website\\Pages\\Main.html";
            string htmlPage = string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(_pathToFolder))
            {
                htmlPage = reader.ReadToEnd();
            }

            ResponseBuilder builder = new PageBuilder()
            {
                Response = new Response()
                {
                    AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork,
                    IsConnected = true,
                    Html = htmlPage,
                    ContentLength = htmlPage.Length,
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
