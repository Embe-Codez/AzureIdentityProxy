using System.Net;
using Azure.Identity;
using AzureIdentityProxyEnhancement.Models;

namespace AzureIdentityProxyEnhancement.Extensions
{
    public class DefaultAzureCredentialOptionsExtension : DefaultAzureCredentialOptions
    {
        public ProxyConfiguration? ProxyConfiguration { get; set; }

        public HttpClientHandler CreateHttpClientHandler()
        {
            if (ProxyConfiguration == null)
            {
                throw new InvalidOperationException("ProxyConfiguration must be set.");
            }

            string proxyUriString = $"{ProxyConfiguration.Address}:{ProxyConfiguration.Port}";
            var proxyUri = new Uri(proxyUriString);
            var proxy = new WebProxy(proxyUri);

            return new HttpClientHandler 
            { 
                Proxy = proxy, 
                UseProxy = true 
            };
        }
    }
}