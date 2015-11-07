using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpServer.Test
{
    [TestClass]
    public class TestWeather
    {
        [TestMethod, TestCategory("TestWeatherAPI")]
        public void TestInitializeWeather()
        {
            WeatherLibrary.Weather weather = new WeatherLibrary.Weather("Kiev");

            var result = weather.GetWeather();
            string res = weather.GetTitle();

            Assert.AreNotEqual(string.Empty, res);
        }
    }
}
