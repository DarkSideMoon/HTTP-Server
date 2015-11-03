using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Net;

namespace HttpServer.Library.Logger
{
    public class ResponseLogger : Log
    {
        private static readonly object SyncObject = new object();

        private string _fileLocation = string.Empty;

        public ResponseLogger(string nameFile)
            : base(nameFile)
        {
            this.FileLocation = Environment.CurrentDirectory;
            this.FileName = nameFile + ".txt";
        }

        public string FileLocation
        {
            get
            {
                return this._fileLocation;
            }
            set
            {
                this._fileLocation = value;
                if (this._fileLocation.LastIndexOf("\\") != (this._fileLocation.Length - 1))
                    this._fileLocation += "\\";
            }
        }

        public override void WriteMessage(Exception message, Log.MessageType type)
        {
            WriteMessage(message, type);
        }

        public override void WriteMessage(string message, Log.MessageType type)
        {
            string newLine = Environment.NewLine;

            FileStream fileStream = null;
            StreamWriter writer = null;
            StringBuilder mess = new StringBuilder();

            try
            {
                fileStream = new FileStream(_fileLocation + this.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(fileStream);

                lock (SyncObject)
                {
                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    // [DateTime] [error/warn] [client 127.0.0.1] 200 (code) - "Url" - "Info system"

                    string banner = string.Format("[" + type.ToString().ToUpper() + "][" + DateTimeLog + "]");
                    string body = message;
                    string end = "----------------------------------------------------";

                    mess.AppendLine(banner + newLine + body + newLine + end);

                    writer.Write(mess);
                }
                writer.Flush();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public override void WriteMessage(TcpClient client, int code)
        {
            string newLine = Environment.NewLine;

            FileStream fileStream = null;
            StreamWriter writer = null;
            StringBuilder mess = new StringBuilder();

            try
            {
                lock (SyncObject)
                {
                    fileStream = new FileStream(_fileLocation + this.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(fileStream);

                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    // [DateTime] [error/warn] [client 127.0.0.1] 200 (code) - "Url" - "Info system"
                    string body = string.Format("[" + this.DateTimeLog + "]"
                                                + "[ " + code + " " + ((HttpStatusCode)code).ToString() + " ]"
                                                + "[client 127.0.0.1:80 ]"
                                                + "[ProtocolType: " + client.Client.ProtocolType + "]" // + "[" + request.HttpMethod + "]"
                                                + "[IsConnected: " + client.Connected + "]"
                                                + "[ " + client.Client.LocalEndPoint + " ]"
                                                + "[" + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() + "]");
                                                //+ "[" + request.Url.AbsoluteUri.ToString() + "]");

                    mess.AppendLine(body);

                    writer.Write(mess);
                }
                writer.Flush();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
