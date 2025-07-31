using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;

namespace ESAPI.Providers
{
    public static class AuthConfigManager
    {
        private static IConfigurationManager<OpenIdConnectConfiguration> configManager;
        private static IConfigurationManager<OpenIdConnectConfiguration> GetConfigurationManager(string metadataAddress)
        {
            if (configManager == null)
            {
                return new ConfigurationManager<OpenIdConnectConfiguration>(metadataAddress, new OpenIdConnectConfigurationRetriever(), new HttpDocumentRetriever());
            }
            return configManager;
        }
        public static OpenIdConnectConfiguration GetMetaData(string metadataAddress)
        {
            var configManager = GetConfigurationManager(metadataAddress);
            var metaData = configManager.GetConfigurationAsync(
                default).Result;
            return metaData;
        }
    }
}
