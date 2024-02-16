using FluentAssertions;
using Xunit;
using AzureIdentityProxyEnhancement.Extensions;
using AzureIdentityProxyEnhancement.Models;

namespace AzureIdentityProxyEnhancement.Tests.ExtensionTests
{
    public class DefaultAzureCredentialOptionsExtensionsTests
    {
        [Theory]
        [InlineData("http://proxy.example.com", 8080)]
        [InlineData("https://anotherproxy.com", 8888)]
        public void ValidProxy_ReturnsHttpClientHandlerWithProxy(string proxyAddress, int proxyPort)
        {
            var proxyConfiguration = new ProxyConfiguration(proxyAddress, proxyPort);
            var credentialOptionsExtension = new DefaultAzureCredentialOptionsExtension
            {
                ProxyConfiguration = proxyConfiguration
            };

            var httpClientHandler = credentialOptionsExtension.CreateHttpClientHandler();

            httpClientHandler.Should().NotBeNull();
            httpClientHandler.Proxy.Should().NotBeNull();
            httpClientHandler.UseProxy.Should().BeTrue();
        }
    }
}