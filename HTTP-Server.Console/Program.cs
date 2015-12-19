using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.ClientLogic;

namespace HttpServer.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Token token = new Token();

            System.Console.WriteLine(token.TokenString);

            System.Threading.Thread.Sleep(new TimeSpan(0, 0, 30));

            System.Console.WriteLine(token.TokenString);

            System.Console.ReadLine();
        }
    }
}
