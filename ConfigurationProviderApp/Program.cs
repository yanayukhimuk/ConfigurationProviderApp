using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;

namespace ConfigurationProviderApp
{
    class Program
    {
        private static string path = "C:\\Users\\ASUS\\Source\\Repos\\ConfigurationProvider\\ConfigurationProviderApp\\MyConfig.json";
        private static MyFileConfigurationProvider _configurationProvider = (MyFileConfigurationProvider)new MyFileConfigurationSource() {Path = path }.Build(null);
        private static MyConfigurationManagerProvider _myConfigurationManagerProvider = new MyConfigurationManagerProvider();
        static void Main(string[] args)
        {
            MyCustomConfig configModel = (_configurationProvider).Get<MyCustomConfig>();
            MyCustomConfig configModel2 = new MyCustomConfig { SomeSetting = "New setting" };
            _configurationProvider.Set(configModel2);

            MyCustomConfig configModel3 = _myConfigurationManagerProvider.Get<MyCustomConfig>();
            MyCustomConfig configModel4 = new MyCustomConfig { MySetting = "Custom Settings" };
            _myConfigurationManagerProvider.Set(configModel4);
        }
    }
}
