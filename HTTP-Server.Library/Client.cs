using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpServer.Library
{
    public class Client
    {
        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public Client(TcpClient client)
        {
            string request = string.Empty;
            byte[] buffer = new byte[1024];
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

                this.SendError(client, 400);
                return;
            }

            // Если строка запроса оканчивается на "/", то добавим к ней index.html
            if (requestUri.EndsWith("/"))
            {
                requestUri += "index.html";
            }

            string f = Environment.CurrentDirectory;
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            string _pathToFolder = wanted_path + "\\Website" + requestUri;

            if (!File.Exists(_pathToFolder))
            {
                this.SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Error! Client: " + client.Client.LocalEndPoint.ToString() + " error numb: " + 404);
                this.ResetConsoleColor();

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

            // Открываем файл, страхуясь на случай ошибки
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
                // Если случилась ошибка, посылаем клиенту ошибку 500
                this.SendError(client, 500);
                return;
            }

            // Посылаем заголовки
            string headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\nContent-Length: " + fileStream.Length + "\n\n";
            byte[] headersBuffer = Encoding.ASCII.GetBytes(headers);
            client.GetStream().Write(headersBuffer, 0, headersBuffer.Length);

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
            // Получаем строку вида "200 OK"
            // HttpStatusCode хранит в себе все статус-коды HTTP/1.1
            string codeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            // Код простой HTML-странички
            string html = "<html><body><h1>" + codeStr + "</h1></body></html>";
            // Необходимые заголовки: ответ сервера, тип и длина содержимого. После двух пустых строк - само содержимое
            string str = "HTTP/1.1 " + codeStr + "\nContent-type: text/html\nContent-Length:" + html.Length.ToString() + "\n\n" + html;
            // Приведем строку к виду массива байт
            byte[] buffer = Encoding.ASCII.GetBytes(str);
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
