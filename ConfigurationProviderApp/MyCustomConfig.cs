using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationProviderApp
{
    internal class MyCustomConfig
    {
        [ConfigurationItem("SomeSetting", typeof(MyFileConfigurationProvider))]
        public string SomeSetting { get; set; }
    }
}
