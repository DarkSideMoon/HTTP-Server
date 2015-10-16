using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.StateServer
{
    public enum RedirectionType
    {
        Found = 302,
        NotModified = 304,
        UseProxy = 305
    }

    public class RedirectionState : State
    {
    }
}
