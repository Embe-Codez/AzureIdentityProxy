using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Moq;
using Xunit;
using AzureIdentityProxyEnhancement.Utils;

namespace AzureIdentityProxyEnhancement.Tests.UtilsTests
{
    public class ProxyUtilsTests
    {
        private const string ValidAddress = "http://proxy.example.com";
        private const int ValidPort = 8080;
        private const string MissingEnvVarErrorMessage = "Proxy URL is not set in the environment variables.";
        private const string MissingConfigErrorMessage = "Proxy URL is not set in the application settings.";

        [Fact]
        public void ValidConfiguration_ReturnsProxyConfiguration()
        {
            var configMock = SetupValidConfiguration();

            var proxyConfiguration = ProxyUtils.LoadProxyConfigurationFromAppSettings(configMock.Object);

            proxyConfiguration.Should().NotBeNull();
                    
            string actualAddress = $"{proxyConfiguration.Address.TrimEnd('/')}:{proxyConfiguration.Port}";
            string expectedAddress = $"{ValidAddress.TrimEnd('/')}:{ValidPort}";

            actualAddress.Should().BeEquivalentTo(expectedAddress);
            proxyConfiguration.Port.Should().Be(ValidPort);
        }

        [Theory]
        [InlineData(null, "8080", "Proxy URL is not set in the environment variables.")]
        [InlineData("http://proxy.example.com", null, "Proxy port is missing or invalid in environment variables.")]
        [InlineData(null, null, "Proxy URL is not set in the environment variables.")]
        public void MissingOrInvalidEnvironmentVariables_ThrowsInvalidOperationException(string envVarUrl, string envVarPort, string expectedErrorMessage)
        {
            SetEnvironmentVariables(envVarUrl, envVarPort);

            Action action = () => ProxyUtils.LoadProxyConfigurationFromEnvironment();
            action.Should().Throw<InvalidOperationException>().WithMessage(expectedErrorMessage);
        }

        [Theory]
        [InlineData("Proxy:Url", null, "Proxy URL is not set in the application settings.")]
        [InlineData("Proxy:Port", null, "Proxy URL is not set in the application settings.")]
        [InlineData("NonExistentKey", null, "Proxy URL is not set in the application settings.")]
        public void InvalidAppSettings_ThrowsInvalidOperationException(string configKey, string configValue, string expectedErrorMessage)
        {
            var configMock = SetupInvalidConfiguration(configKey, configValue);

            Action action = () => ProxyUtils.LoadProxyConfigurationFromAppSettings(configMock.Object);
            action.Should().Throw<InvalidOperationException>().WithMessage(expectedErrorMessage);
        }

        private void SetEnvironmentVariables(string envVarUrl, string envVarPort)
        {
            Environment.SetEnvironmentVariable("PROXY_URL", envVarUrl);
            Environment.SetEnvironmentVariable("PROXY_PORT", envVarPort);
        }

       private Mock<IConfiguration> SetupValidConfiguration()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x["Proxy:Url"]).Returns(ValidAddress.TrimEnd('/'));
            configMock.Setup(x => x["Proxy:Port"]).Returns(ValidPort.ToString());
            return configMock;
        }

        private Mock<IConfiguration> SetupInvalidConfiguration(string configKey, string configValue)
        {
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(x => x[configKey]).Returns(configValue);
            return configMock;
        }
    }
}