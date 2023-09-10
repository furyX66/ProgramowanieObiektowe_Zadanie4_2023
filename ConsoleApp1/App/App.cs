using app.Services;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Metrics;

namespace app.App
{
    public class App
    {
        private static AppBuilder _appBuilder = new AppBuilder();
        static void Main(string[] args)
        {
            _appBuilder.Menu();
        }
    }
}

