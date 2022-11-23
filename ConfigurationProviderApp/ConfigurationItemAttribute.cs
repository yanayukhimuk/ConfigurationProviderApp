using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationProviderApp
{
    [AttributeUsage(AttributeTargets.All)]
    public class ConfigurationItemAttribute : Attribute
    {
        public readonly string PropertyName;
        public readonly Type Type;

        public ConfigurationItemAttribute(string propertyName, Type type)
        {
            this.PropertyName = propertyName;
            this.Type = type;
        }
    }
}
