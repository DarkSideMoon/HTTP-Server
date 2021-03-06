﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HttpServer.Library.Logger;

namespace HttpServer.Library
{
    public enum ServerTypeError
    {
        ExpiredSession = 1,
        NotAuthorized = 2,
        PageNotFound = 3,
        ServerError = 4,
        ValidationError = 5
    }

    public class Server
    {
        private Log _logger;
        private State _state;

        private TcpListener _listener;

        public Server(int port)
        {
            this._logger = new MessageLogger("MessageLogger");
            this._listener = new TcpListener(IPAddress.Any, port);
            this._listener.Start();

            // **************************** Start Initialize searcher of folders ****************************



            // **************************** End Initialize searcher of folders ****************************
            this.SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine("The server is starting...");
            Console.WriteLine("Listening on http://127.0.0.1:" + port);
            this.ResetConsoleColor();
            Console.WriteLine("Current Time: " + DateTime.Now.ToString());

            // Get new client
            TcpClient _client = this._listener.AcceptTcpClient();

            this.SetConsoleColor(ConsoleColor.Green);
            Console.WriteLine("The server is started! Address: " + _client.Client.LocalEndPoint.ToString());
            this.ResetConsoleColor();

            this._logger.WriteMessage("The server is starting! Address : " + _client.Client.LocalEndPoint.ToString(), Log.MessageType.Info);


            while (true)
                  ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), this._listener.AcceptTcpClient());

            // Create new thread
            //Thread thread = new Thread(new ParameterizedThreadStart(ClientThread));
            // Start new thread with getting client
            //thread.Start(client);

            //thread.Join(); handlers tornado
        }

        // Остановка сервера
        ~Server()
        {
            // Если "слушатель" был создан
            if (this._listener != null)
            {
                // Остановим его
                this._listener.Stop();
            }
        }

        public int MaxSimultaneousConnections { get; set; }
        public int ExpirationTimeSeconds { get; set; }

        /// <summary>
        /// State of the server
        /// </summary>
        public State State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        public void Work()
        {
            // the server is start and working now :)
        }

        private static void ClientThread(object stateInfo)
        {
            new Client((TcpClient)stateInfo);
        }

        /// <summary>
        /// Get locals hosts ips addresses
        /// </summary>
        /// <returns></returns>
        private static List<IPAddress> GetLocalHostIPs()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            List<IPAddress> result = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();
            return result;
        }

        #region Methods for work with console

        private void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private void ResetConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion
    }
}
