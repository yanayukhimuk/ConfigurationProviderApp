using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace ConfigurationProviderApp
{
    class Program
    {
        private static MyFileConfigurationProvider configurationProvider;

        internal static MyFileConfigurationProvider ConfigurationProvider { get => configurationProvider; set => configurationProvider = value; }

        static void Main(string[] args)
        {
            MyCustomConfig configModel = (configurationProvider).Get<MyCustomConfig>();
            MyCustomConfig configModel2 = new MyCustomConfig { SomeSetting = "New setting" };
            configurationProvider.Set(configModel2);

            var value1 = configModel2.SomeSetting;
        }
    }
}
