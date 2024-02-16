using FluentAssertions;
using Xunit;
using AzureIdentityProxyEnhancement.Models;
using System;

namespace AzureIdentityProxyEnhancement.Tests.ModelsTests
{
    public class ProxyConfigurationTests
    {
        private const string ValidAddress = "http://proxy.example.com";
        private const int ValidPort = 8080;
        private const string InvalidAddress = "invalid";
        private const int InvalidPort = -1;
        private const string ValidAddressErrorMessage = "Proxy address must be a valid HTTP or HTTPS URL.";
        private const string ValidPortErrorMessage = "Port must be a positive number between 1 and 65535.";

        [Theory]
        [InlineData(null, ValidPort, ValidAddressErrorMessage)]
        [InlineData(InvalidAddress, ValidPort, ValidAddressErrorMessage)]
        [InlineData(ValidAddress, InvalidPort, ValidPortErrorMessage)]
        [InlineData(ValidAddress, 0, ValidPortErrorMessage)]
        [InlineData(ValidAddress, 65536, ValidPortErrorMessage)]
        public void InvalidInput_ThrowsArgumentException(string address, int port, string expectedErrorMessage)
        {
            Action createInvalidProxyConfiguration = () => new ProxyConfiguration(address, port);

            createInvalidProxyConfiguration.Should().Throw<ArgumentException>()
                .WithMessage($"*{expectedErrorMessage}*");
        }
    }
}