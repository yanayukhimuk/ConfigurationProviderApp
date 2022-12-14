using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace ConfigurationProviderApp
{
    public class MyFileConfigurationProvider : FileConfigurationProvider
    {
        Dictionary<string, string> configuration = new Dictionary<string, string>();    
        public MyFileConfigurationProvider(MyFileConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            configuration = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
        }

        public override void Set(string key, string value)
        {
            var fieldsToRead = typeof(MyCustomConfig).GetFields().Where(x =>
                x.CustomAttributes.OfType<ConfigurationItemAttribute>().FirstOrDefault() is not null);

            foreach (var field in fieldsToRead)
            {
                var attr = field.CustomAttributes.OfType<ConfigurationItemAttribute>().First();

                if (configuration.ContainsKey(attr.PropertyName))
                {
                    Data[field.Name] = configuration[attr.PropertyName];
                }
            }
        }

        public override bool TryGet(string key, out string value)
        {
            //var fileds = configuration as Dictionary<string, string>;
            return Data.TryGetValue(key, out value);
        }
    }
}
