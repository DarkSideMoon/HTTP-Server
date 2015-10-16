using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.StateServer
{
    public enum ClientErrorType
    {
        BadRequest = 400, // Не правильный запрос
        Forbidden = 403, // Запрещено 
        NotFound = 404, // Не найдено 
        MethodNotAllowed = 405, // Метод не поддерживается 
        RequestTimeout = 408, // Истекло время ожидания
        RequestEntityTooLarge = 413, // размер запроса слишком велик
        RequestURITooLarge = 414, // запрашиваемый URI слишком длинный
        IamTeapot = 418, // Я чайник
        Locked = 423,
        UnrecoverableError = 456 // некорректируемая ошибка
    }

    public class ClientErrorState : State 
    {
    }
}
