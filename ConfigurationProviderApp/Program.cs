using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConfigurationProviderApp
{
    class Program
    {
        //private static string path = "C:\\Users\\ASUS\\Source\\Repos\\ConfigurationProvider\\ConfigurationProviderApp\\MyConfig.json";
        //private static MyFileConfigurationProvider _configurationProvider = (MyFileConfigurationProvider)new MyFileConfigurationSource() {Path = path }.Build(null);
        //private static MyConfigurationManagerProvider _myConfigurationManagerProvider = new MyConfigurationManagerProvider();

        private static List<IConfigurationSource> pluginSources = new List<IConfigurationSource>();
        static void Main(string[] args)
        {
            if (Directory.Exists("plugins"))
            {
                var files = Directory.GetFiles("plugins");
                foreach (var plugin in files)
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(plugin));
                    var providers = assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(MyFileConfigurationSource)));
                    foreach (var x in providers)
                    {
                        var provider = Activator.CreateInstance(x) as IConfigurationSource;
                        pluginSources.Add(provider);
                    }
                }
            }

            ConfigurationBuilder builder = new ConfigurationBuilder();

            foreach (var plugin in pluginSources)
            {
                plugin.Build(builder);
            }

            IConfiguration c = builder.Build();

            //MyCustomConfig configModel = (_configurationProvider).Get<MyCustomConfig>();
            //MyCustomConfig configModel2 = new MyCustomConfig { SomeSetting = "New setting" };
            //_configurationProvider.Set(configModel2);

            //MyCustomConfig configModel3 = _myConfigurationManagerProvider.Get<MyCustomConfig>();
            //MyCustomConfig configModel4 = new MyCustomConfig { MySetting = "Custom Settings" };
            //_myConfigurationManagerProvider.Set(configModel4);
        }
    }
}
