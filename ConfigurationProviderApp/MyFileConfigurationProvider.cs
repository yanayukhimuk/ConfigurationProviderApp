using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;

namespace ConfigurationProviderApp
{
    class MyFileConfigurationProvider : FileConfigurationProvider
    {
        public MyFileConfigurationProvider(FileConfigurationSource source) : base (source)
        {

        }
        public override void Load(Stream stream)
        {
            var fields = typeof(MyCustomConfig).GetFields(BindingFlags.Public)
                .Select(x => (x, x.CustomAttributes.OfType<ConfigurationItemAttribute>().FirstOrDefault()))
                .Where(x => x.Item2 != null);

            var jobject = JsonNode.Parse(stream);
            MyCustomConfig config = new ();

            foreach (var field in fields)
            {
                field.x.SetValue(config, jobject[field.Item2.PropertyName]);
            }
        }
    }
}
