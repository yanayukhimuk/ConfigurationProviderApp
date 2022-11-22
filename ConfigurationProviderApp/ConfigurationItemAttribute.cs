using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationProviderApp
{
    [AttributeUsage(AttributeTargets.All)]
    public class ConfigurationItemAttribute : Attribute
    {
        public string PropertyName { get; }
        public Type Type1 { get; }

        public ConfigurationItemAttribute(string propertyName, Type type)
        {
            PropertyName = propertyName;
            Type1 = type;
        }
    }
}
