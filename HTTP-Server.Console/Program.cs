using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Library.ClientLogic;
using HttpServer.Library.Composite;

namespace HttpServer.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Token token = new Token();
            //System.Console.WriteLine(token.TokenString);
            //System.Threading.Thread.Sleep(new TimeSpan(0, 0, 30));
            //System.Console.WriteLine(token.TokenString);
            MenuElement root = new Composite("Root folder");

            MenuElement home = new MainPage("Home");
            MenuElement main = new MainPage("Main");

            MenuElement rootHome = new Composite("Home pages");
            MenuElement rootMain = new Composite("Main pages");

            MenuElement weather = new Page("Weather Page");
            MenuElement ip = new Page("Ip config page");
            MenuElement json = new Page("Json page");
            MenuElement logIn = new Page("Log In page");
            MenuElement checkIn = new Page("Check In page");
            MenuElement info = new Page("Information page");

            // Pages that avaiable use in home
            rootHome.Add(weather);
            rootHome.Add(ip);
            rootHome.Add(json);
            rootHome.Add(logIn);
            rootHome.Add(checkIn);
            rootHome.Add(info);

            // Pages that avaiable use after registration and login
            rootMain.Add(info);

            root.Add(rootMain);
            root.Add(rootHome);

            //root.Display(0);
            root.Display(1);


            System.Console.ReadLine();
        }
    }
}
