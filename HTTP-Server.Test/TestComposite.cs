using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpServer.Library.Composite;

namespace HttpServer.Test
{
    [TestClass]
    public class TestComposite
    {
        [TestMethod]
        public void TestCompositeAdd()
        {
            MenuElement root = new Composite("Root folder");
            MenuElement home = new MainPage("Home");
            MenuElement main = new MainPage("Main");
            MenuElement weather = new Page("Weather Page");
            MenuElement element2 = new Page("Page2");

            root.Add(home);
            root.Add(main);

            root.Display(1);
        }
    }
}
