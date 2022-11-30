using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
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
        private static List<IConfigurationSource> pluginSources = new();
        static void Main(string[] args)
        {
            if (Directory.Exists("plugins"))
            {
                var files = Directory.GetFiles("plugins"); // extensions check
                foreach (var plugin in files)
                {
                    var fileInfo = new FileInfo(plugin);
                    var fileExtension = fileInfo.Extension;
                    Assembly assembly;
                    IEnumerable<Type> providers;

                    if (fileExtension == ".dll")
                    {
                        assembly = Assembly.Load(File.ReadAllBytes(plugin));
                        providers = assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IConfigurationSource)));
                        foreach (var prov in providers)
                        {
                            var provider = Activator.CreateInstance(prov) as IConfigurationSource;
                            pluginSources.Add(provider);
                        }
                    }
                    else
                        continue;
                }
            }
            else
            {
                Console.WriteLine("The directory wasn't found!");
            }

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetFileProvider(new PhysicalFileProvider(Environment.CurrentDirectory));

            IConfigurationSource source = default;
            foreach (var plugin in pluginSources)
            {
                builder.Add(plugin);
                source = plugin;
            }

            IConfiguration config = builder.Build();
        }
    }
}
