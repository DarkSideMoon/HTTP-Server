using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

using HttpServer.Library.Logger;
using HttpServer.Library.ResponseServer;
using HttpServer.Library.RouteFolder;
using HttpServer.Library.StateServer;
using HttpServer.Library.MediatorClient;
using HttpServer.Library.ClientLogic;

namespace HttpServer.Library
{
    #region Feature List
    // HTTP-server (state,builder,factory method, mediator, composite)
    // Authentication Basic Digest
    // Cookies authentication
    // Give the permanent time to auth
    #endregion
    public class Client
    {
        #region Fields
        private Log _logger;
        private Route _route;
        private FileStream fileStream;
        private Mediator _mediator;
        private State _stateError;
        private User _user;

        private string request;
        private byte[] buffer;
        private int count;
        #endregion

        #region Constructor
        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient client)
        {
            this.Initialize(client);

            while ((this.count = client.GetStream().Read(this.buffer, 0, this.buffer.Length)) > 0)
            {
                this.request += Encoding.ASCII.GetString(this.buffer, 0, this.count);
                if (this.request.IndexOf("\r\n\r\n") >= 0 || this.request.Length > 4096)
                    break;
            }
            
            // CHECK COOKIES FOR USER INSTANCE -------------

            // Парсим строку запроса с использованием регулярных выражений
            // При этом отсекаем все переменные GET-запроса
            Match reqMatch = Regex.Match(this.request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            if(this.request.IsJson())
            {
                _user = _user.RegistrationUser(this.request);
            }

            if (reqMatch == Match.Empty)
            {
                this.WriteConsoleMessage(client, 400);
                //this._logger.WriteMessage(client, 400);
                this.SendError(client, 400);
                return;
            }
            // Получаем строку запроса
            string requestUri = reqMatch.Groups[1].Value;

            // **********************Routing URL**********************
            this._route = new Route(requestUri, client);
            // The object is deleted by carabidge collector if not using this if statment
            if (this._route.IsRouting == true)
            {
                // Detected what is query and create a response to client
                this._route.Send(this._route.Action);
                return;
            }
            // Приводим ее к изначальному виду, преобразуя экранированные символы
            // Например, "%20" -> " "
            requestUri = Uri.UnescapeDataString(requestUri);

            // Если в строке содержится двоеточие, передадим ошибку 400
            // Это нужно для защиты от URL типа http://example.com/../../file.txt
            if (requestUri.IndexOf("..") >= 0)
            {
                this.WriteConsoleMessage(client, 400);
                //this._logger.WriteMessage(client, 400);
                this.SendError(client, 400);
                return;
            }
            // Если строка запроса оканчивается на "/", то добавим к ней index.html
            if (requestUri.EndsWith("/"))
                requestUri += "index.html";

            // Find the path of the directory then website is hosting
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFolder = wanted_path + "\\Website" + requestUri;

            if (!File.Exists(_pathToFolder))
            {
                this.WriteConsoleMessage(client, 404);
                //this._logger.WriteMessage(client, 404);
                this._mediator.Send(404, this._stateError); // Null reference exception
                //this.SendError(client, 404);
                return;
            }
            // Получаем расширение файла из строки запроса
            string extension = requestUri.Substring(requestUri.LastIndexOf('.'));
            string contentType = this.DetectTypeByExtension(extension);

            try
            {
                this.fileStream = new FileStream(_pathToFolder, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                this.WriteConsoleMessage(client, 500);
                //this._logger.WriteMessage(client, 500);
                this.SendError(client, 500);
                return;
            }
            // Посылаем заголовки
            string headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\nContent-Length: " + this.fileStream.Length + "\n\n";
            byte[] headersBuffer = Encoding.ASCII.GetBytes(headers);
            client.GetStream().Write(headersBuffer, 0, headersBuffer.Length);

            // this._logger.WriteMessage(client, 200);
            // Пока не достигнут конец файла
            while (this.fileStream.Position < this.fileStream.Length)
            {
                // Читаем данные из файла
                this.count = this.fileStream.Read(this.buffer, 0, this.buffer.Length);
                // И передаем их клиенту
                client.GetStream().Write(this.buffer, 0, this.count);
            }
            // Закроем файл и соединение
            this.fileStream.Close();
            client.Close();
        }
        #endregion

        private void SendError(TcpClient client, int code)
        {
            string codeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            Quote q = new Quote();

            string html = "<html>" +
                                "<body>" +
                                    "<h1>" + codeStr + "</h1>" +
                                    "<h3>Quote of the day</h3>" +
                                    "<blockquote><h3><i>" + q.GetQuote() + "</i></h3></blockquote>" +
                                    "<h3>Черномырдин Виктор Степанович</h3>" +
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
                    StatusDesc = ((HttpStatusCode)code).ToString(),
                    ContentType = "text/html",
                    StatusCode = code,
                    Charset = System.Text.Encoding.UTF8,
                    DateTimeResponse = DateTime.Now,
                    ProtocolType = System.Net.Sockets.ProtocolType.Tcp
                }
            };

            string str = builder.CreateResponse();

            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            // Отправим его клиенту
            client.GetStream().Write(buffer, 0, buffer.Length);
            // Закроем соединение
            client.Close();
        }

        #region Server Methods
        private void Initialize(TcpClient client)
        {
            // Initialize classes
            this._user = new User();

            this._mediator = new ClientMediator();
            this._mediator.Client = client;
            this._stateError = new HttpError(this._mediator);

            this._logger = new ResponseLogger("ResponseLogger");

            // Initialize varianles
            this.request = string.Empty;
            this.buffer = new byte[2048]; //1024
        }

        private string DetectTypeByExtension(string extension)
        {
            // Тип содержимого
            string contentType = string.Empty;
            // Пытаемся определить тип содержимого по расширению файла
            switch (extension)
            {
                case ".htm":
                case ".html":
                    return contentType = "text/html";
                case ".css":
                    return contentType = "text/stylesheet";
                case ".js":
                    return contentType = "text/javascript";
                case ".jpg":
                    return contentType = "image/jpeg";
                case ".jpeg":
                case ".png":
                case ".gif":
                    return contentType = "image/" + extension.Substring(1);
                default:
                    if (extension.Length > 1)
                        return contentType = "application/" + extension.Substring(1);
                    else
                        return contentType = "application/unknown";
            }
        }
        #endregion

        #region Methods for work with console
        private void WriteConsoleMessage(TcpClient client, int errorNumb)
        {
            this.SetConsoleColor(ConsoleColor.Red);
            Console.WriteLine("Error! Client: " + client.Client.LocalEndPoint.ToString() + " error numb: " + errorNumb);
            this.ResetConsoleColor();
        }
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
