using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.StateServer
{
    public enum SuccessType
    {
        Ok = 200,
        Created = 201,
        Accepted = 202
    }

    public class SuccessState : State
    {
    }
}
