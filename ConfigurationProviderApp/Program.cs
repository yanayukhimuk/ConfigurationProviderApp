using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace ConfigurationProviderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MemberInfo info = typeof(ConfigurationItemAttribute);
            IConfigurationBuilder cb = new ConfigurationBuilder();
            cb.Add(new MyFileConfigurationSource() { Optional = false, Path = "C:\\Users\\Yana_Yukhimuk\\source\\repos\\ConfigurationProviderApp\\ConfigurationProviderApp\\MyConfig.json" });

            _ = cb.Build();
        }
    }
}
