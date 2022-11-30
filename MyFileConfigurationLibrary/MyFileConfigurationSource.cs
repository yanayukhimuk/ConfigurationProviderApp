using Microsoft.Extensions.Configuration;

namespace ConfigurationProviderApp
{
    public class MyFileConfigurationSource : FileConfigurationSource
    {
        public readonly string? FilePath = "appsettings.json";
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            Path = FilePath;

            FileProvider = FileProvider ?? builder.GetFileProvider();
            return new MyFileConfigurationProvider(this);
        }
    }
}
