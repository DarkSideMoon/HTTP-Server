using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HttpServer.Library.Logger;
using HttpServer.Library.ResponseServer;

namespace HttpServer.Library
{
    // HTTP-server (state,builder,factory method, mediator, composite)
    // Authentication Basic Digest
    public class Client
    {
        private Log _logger;

        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient client)
        {
            this._logger = new ResponseLogger("ResponseLogger");

            string request = string.Empty;
            byte[] buffer = new byte[2048]; //1024
            int count;

            while ((count = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, count);
                if (request.IndexOf("\r\n\r\n") >= 0 || request.Length > 4096)
                    break;
            }
            // Парсим строку запроса с использованием регулярных выражений
            // При этом отсекаем все переменные GET-запроса
            Match reqMatch = Regex.Match(request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");

            if (reqMatch == Match.Empty)
            {
                this.SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Error! Client: " + client.Client.LocalEndPoint.ToString() + " error numb: " + 400);
                this.ResetConsoleColor();

                //this._logger.WriteMessage(client, 400);
                this.SendError(client, 400);
                return;
            }
            // Получаем строку запроса
            string requestUri = reqMatch.Groups[1].Value;

            // Приводим ее к изначальному виду, преобразуя экранированные символы
            // Например, "%20" -> " "
            requestUri = Uri.UnescapeDataString(requestUri);

            // Если в строке содержится двоеточие, передадим ошибку 400
            // Это нужно для защиты от URL типа http://example.com/../../file.txt
            if (requestUri.IndexOf("..") >= 0)
            {
                this.SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Error! Client: " + client.Client.LocalEndPoint.ToString() + " error numb: " + 400);
                this.ResetConsoleColor();

                //this._logger.WriteMessage(client, 400);
                this.SendError(client, 400);
                return;
            }

            // Если строка запроса оканчивается на "/", то добавим к ней index.html
            if (requestUri.EndsWith("/"))
            {
                requestUri += "index.html";
            }

            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFolder = wanted_path + "\\Website" + requestUri;

            if (!File.Exists(_pathToFolder))
            {
                this.SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Error! Client: " + client.Client.LocalEndPoint.ToString() + " error numb: " + 404);
                this.ResetConsoleColor();

                //this._logger.WriteMessage(client, 404);
                this.SendError(client, 404);
                return;
            }
            // Получаем расширение файла из строки запроса
            string extension = requestUri.Substring(requestUri.LastIndexOf('.'));
            // Тип содержимого
            string contentType = string.Empty;
            // Пытаемся определить тип содержимого по расширению файла
            switch (extension)
            {
                case ".htm":
                case ".html":
                    contentType = "text/html";
                    break;
                case ".css":
                    contentType = "text/stylesheet";
                    break;
                case ".js":
                    contentType = "text/javascript";
                    break;
                case ".jpg":
                    contentType = "image/jpeg";
                    break;
                case ".jpeg":
                case ".png":
                case ".gif":
                    contentType = "image/" + extension.Substring(1);
                    break;
                default:
                    if (extension.Length > 1)
                        contentType = "application/" + extension.Substring(1);
                    else
                        contentType = "application/unknown";
                    break;
            }
            FileStream fileStream;
            try
            {
                fileStream = new FileStream(_pathToFolder, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                this.SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Error! Client: " + client.Client.LocalEndPoint.ToString() + " error numb: " + 500);
                this.ResetConsoleColor();

                //this._logger.WriteMessage(client, 500);
                this.SendError(client, 500);
                return;
            }
            // Посылаем заголовки
            string headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\nContent-Length: " + fileStream.Length + "\n\n";
            byte[] headersBuffer = Encoding.ASCII.GetBytes(headers);
            client.GetStream().Write(headersBuffer, 0, headersBuffer.Length);

            //this._logger.WriteMessage(client, 200);
            // Пока не достигнут конец файла
            while (fileStream.Position < fileStream.Length)
            {
                // Читаем данные из файла
                count = fileStream.Read(buffer, 0, buffer.Length);
                // И передаем их клиенту
                client.GetStream().Write(buffer, 0, count);
            }
            // Закроем файл и соединение
            fileStream.Close();
            client.Close();
        }

        // Отправка страницы с ошибкой
        private void SendError(TcpClient client, int code)
        {
            string codeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            //string html = "<html><body><h1>" + codeStr + "</h1></body></html>";

            //string str = "HTTP/1.1 " + codeStr + "\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;

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
