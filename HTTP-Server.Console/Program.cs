using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library;

namespace HttpServer.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server(80);

            System.Console.ReadLine();
        }
    }
}
