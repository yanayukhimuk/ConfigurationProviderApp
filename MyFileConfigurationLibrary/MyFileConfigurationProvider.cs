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
        public MyFileConfigurationProvider(FileConfigurationSource source) : base (source)
        {
            Data = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(source.Path));
        }
        public override void Load(Stream stream)
        {
            Data = JsonSerializer.Deserialize<Dictionary<string, string>>(stream);
        }

        public T Get<T>() where T : class, new()
        {
            T configModel = new T();

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            foreach (var property in typeof(T).GetProperties(flags)) 
            {
                foreach(var attribute in property.GetCustomAttributes())
                {
                    if (attribute is ConfigurationItemAttribute itemAttribute) 
                    {
                        if(itemAttribute.Type != typeof(MyFileConfigurationProvider)) 
                        {
                            continue;
                        }

                        string propertyName = Data[itemAttribute.PropertyName];
                        property.SetValue(configModel, Convert.ChangeType(propertyName, property.PropertyType));
                    }
                }
            }
            return configModel; 
        }

        public void Set <T>(T model)
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
 
            foreach (var property in typeof(T).GetProperties(flags)) 
            {
                foreach (var attribute in property.GetCustomAttributes())
                {
                    if (attribute is ConfigurationItemAttribute itemAttribute)
                    {
                        if (itemAttribute.Type != typeof(MyFileConfigurationProvider))
                        {
                            continue;
                        }

                        Data[itemAttribute.PropertyName] = property.GetValue(model).ToString();
                    }
                }
            }
            string newConfig = JsonSerializer.Serialize(Data);
            File.WriteAllText(Source.Path, newConfig);
        }
    }
}
