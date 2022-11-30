using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ConfigurationProviderApp
{
    public class MyFileConfigurationProvider : FileConfigurationProvider
    {
        public MyFileConfigurationProvider(MyFileConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            var obj = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);

            var fieldToRead = typeof(MyCustomConfig).GetFields().Where(x =>
                x.CustomAttributes.OfType<ConfigurationItemAttribute>().FirstOrDefault() is not null);

            foreach (var field in fieldToRead)
            {
                var attr = field.CustomAttributes.OfType<ConfigurationItemAttribute>().First();

                if (obj.ContainsKey(attr.PropertyName))
                {
                    Data[field.Name] = obj[attr.PropertyName];
                }
            }
        }

        public override void Set(string key, string value)
        {
            base.Set(key, value);
        }

        public override bool TryGet(string key, out string value)
        {
            return base.TryGet(key, out value);
        }
    }
}
