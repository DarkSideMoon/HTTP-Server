using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.StateServer
{
    public enum ServerErrorType
    {
        InternalServerError = 500, // Внутренная ошибка 
        NotImplemented = 501, // Не Реализовано 
        BadGateway = 502, // Ошибочный путь 
        ServiceUnavailable = 503, // Сервис не доступен 
        HTTPVersionNotSupported = 505, // версия HTTP не поддерживается 
    }

    public class ServerErrorState : State
    {
    }
}
