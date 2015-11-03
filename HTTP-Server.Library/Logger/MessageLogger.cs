using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.Logger
{
    public class MessageLogger : Log
    {
        private static readonly object SyncObject = new object();

        private string _fileLocation = string.Empty;

        public MessageLogger(string nameFile)
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
                lock (SyncObject)
                {
                    fileStream = new FileStream(_fileLocation + this.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    writer = new StreamWriter(fileStream);

                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    string body = string.Format("[" + this.DateTimeLog + "]"
                                                + "[" + type + "]"
                                                + "[" + message + "]");

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

        public override void WriteMessage(TcpClient request, int code)
        {
            throw new NotImplementedException();
        }
    }
}
