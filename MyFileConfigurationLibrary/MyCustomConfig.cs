using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationProviderApp
{
    public class MyCustomConfig
    {
        [ConfigurationItem("SomeSetting", typeof(MyFileConfigurationProvider))]
        public string SomeSetting { get; set; }

        [ConfigurationItem("MySetting", typeof(MyFileConfigurationProvider))]
        public string MySetting { get; set; }
    }
}
