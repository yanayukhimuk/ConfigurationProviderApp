using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ConfigurationProviderApp
{
    public class MyConfigurationManagerProvider 
    {
        private Configuration Data;
        public MyConfigurationManagerProvider() 
        {
            Data = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        }

        public T Get<T>() where T : class, new()
        {
            T configModel = new();

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

            foreach (var property in typeof(T).GetProperties(flags)) 
            {
                foreach(var attribute in property.GetCustomAttributes())
                {
                    if (attribute is ConfigurationItemAttribute itemAttribute) 
                    {
                        if(itemAttribute.Type != typeof(MyConfigurationManagerProvider)) 
                        {
                            continue;
                        }

                        string propertyName = Data.AppSettings.Settings[itemAttribute.PropertyName].Value;
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
                        if (itemAttribute.Type != typeof(MyConfigurationManagerProvider))
                        {
                            continue;
                        }

                        Data.AppSettings.Settings[itemAttribute.PropertyName].Value = property.GetValue(model).ToString();
                    }
                }
            }
            Data.Save(ConfigurationSaveMode.Full);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
