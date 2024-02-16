using Microsoft.Extensions.Configuration;
using AzureIdentityProxyEnhancement.Models;

namespace AzureIdentityProxyEnhancement.Utils
{
    public static class ProxyUtils
    {
        public static ProxyConfiguration LoadProxyConfigurationFromEnvironment()
        {
            var proxyUrl = Environment.GetEnvironmentVariable("PROXY_URL");
            var proxyPort = Environment.GetEnvironmentVariable("PROXY_PORT");

            if (string.IsNullOrWhiteSpace(proxyUrl))
            {
                throw new InvalidOperationException("Proxy URL is not set in the environment variables.");
            }

            if (!int.TryParse(proxyPort, out int port) || port <= 0 || port > 65535)
            {
                throw new InvalidOperationException("Proxy port is missing or invalid in environment variables.");
            }

            return new ProxyConfiguration(proxyUrl, port);
        }

        public static ProxyConfiguration LoadProxyConfigurationFromAppSettings(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Configuration must not be null.");
            }

            var proxyUrl = configuration["Proxy:Url"];
            var proxyPortValue = configuration["Proxy:Port"];

            if (string.IsNullOrWhiteSpace(proxyUrl))
            {
                throw new InvalidOperationException("Proxy URL is not set in the application settings.");
            }

            if (!int.TryParse(proxyPortValue, out int port) || port <= 0 || port > 65535)
            {
                throw new InvalidOperationException("Proxy port is missing or invalid in application settings.");
            }

            return new ProxyConfiguration(proxyUrl, port);
        }
    }
}