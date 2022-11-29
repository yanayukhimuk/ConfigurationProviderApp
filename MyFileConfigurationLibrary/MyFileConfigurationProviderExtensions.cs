using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationProviderApp
{
    internal static class MyFileConfigurationProviderExtensions
    {
        public static IConfigurationBuilder AddMyConfiguration(this IConfigurationBuilder builder, string path)
        {
            builder.Add(new MyFileConfigurationSource(path));
            return builder;
        }
    }
}
