using System;
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
    public class Server
    {
        private static HttpListener listener;
        private static Semaphore sem;

        private static RequestLogger _logger = new RequestLogger("RequestLogger");
        private State _state;

        public Server()
        {
            this.MaxSimultaneousConnections = 20;
            this.ExpirationTimeSeconds = 60;
            this.ValidationTokenName = "__AdrinToken__";

            sem = new Semaphore(this.MaxSimultaneousConnections, this.MaxSimultaneousConnections);
        }

        public static List<IPAddress> GetLocalHosts
        {
            get { return GetLocalHostIPs(); }
        }

        public static bool IsStarted
        {
            get { return listener.IsListening; }
        }

        public int MaxSimultaneousConnections { get; set; }
        public int ExpirationTimeSeconds { get; set; }
        public string ValidationTokenName { get; set; }

        /// <summary>
        /// State of the server
        /// </summary>
        public State State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        /// <summary>
        /// Starts the web server.
        /// </summary>
        public static void Start()
        {
            List<IPAddress> localHostIPs = GetLocalHostIPs();
            HttpListener listener = InitializeListener(localHostIPs);
            Start(listener);
        }

        /// <summary>
        /// Begin listening to connections on a separate worker thread.
        /// </summary>
        private static void Start(HttpListener listener)
        {
            listener.Start();
            Task.Run(() => RunServer(listener));
        }

        /// <summary>
        /// Start awaiting for connections, up to the "maxSimultaneousConnections" value.
        /// This code runs in a separate thread.
        /// </summary>
        private static void RunServer(HttpListener listener)
        {
            while (true)
            {
                sem.WaitOne();
                StartConnectionListener(listener);
            }
        }

        /// <summary>
        /// Await connections.
        /// </summary>
        private static async void StartConnectionListener(HttpListener listener)
        {
            // Wait for a connection. Return to caller while we wait.
            HttpListenerContext context = await listener.GetContextAsync();

            // Write to logger the request
            _logger.WriteMessage(context.Request);

            // Release the semaphore so that another listener can be immediately started up.
            sem.Release();
            HttpListenerRequest request = context.Request;

            // We have a connection, do something...
            string response = "Hello Browser!";
            byte[] encoded = Encoding.UTF8.GetBytes(response);
            context.Response.ContentLength64 = encoded.Length;
            context.Response.OutputStream.Write(encoded, 0, encoded.Length);
            context.Response.OutputStream.Close();
        }

        private static List<IPAddress> GetLocalHostIPs()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            List<IPAddress> result = host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();
            return result;
        }

        private static HttpListener InitializeListener(List<IPAddress> localHostIPs)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost/");

            localHostIPs.ForEach(ip =>
                {
                    Console.WriteLine("Listening on IP " + "http://" + ip.ToString() + "/");
                    listener.Prefixes.Add("http://" + ip.ToString() + "/");
                });

            return listener;
        }
    }
}
