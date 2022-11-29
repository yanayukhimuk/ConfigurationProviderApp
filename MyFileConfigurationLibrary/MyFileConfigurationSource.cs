using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationProviderApp
{
    public class MyFileConfigurationSource : FileConfigurationSource
    {
        private readonly string? _path;
        public MyFileConfigurationSource (string? path) => _path = path;    
        public override IConfigurationProvider Build(IConfigurationBuilder builder) =>
            new MyFileConfigurationProvider();
    }
}
