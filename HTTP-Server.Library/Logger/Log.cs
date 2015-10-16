using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.Logger
{
    public abstract class Log
    {
        public Log(string nameFile)
        {
            this.FileName = nameFile;
            this.DateTimeLog = DateTime.Now;
        }

        public enum MessageType
        {
            Info = 1,
            Warning = 2,
            Error = 3
        }

        public string FileName { get; set; }

        public DateTime DateTimeLog { get; set; }

        public abstract void WriteMessage(Exception message, MessageType type);

        public abstract void WriteMessage(string message, MessageType type);

        public abstract void WriteMessage(HttpListenerRequest request);
    }
}
