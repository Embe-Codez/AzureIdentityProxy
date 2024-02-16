# Azure Identity Proxy Enhancement

This .NET library simplifies setting a proxy for the Azure Identity `DefaultAzureCredential` class. It includes classes and extensions to manage proxy configurations effectively.

**Please Note:** This library is not published on NuGet.org and is not intended for production environments. It was created to address a specific issue and may not have undergone extensive testing or maintenance. Use it at your own discretion.

## Installation

You can install the library locally by downloading the repository and referencing it in your project.

```xml
<ProjectReference Include="path/to/AzureIdentityProxyEnhancement.csproj" />
```

## Usage
### Setting Proxy Configuration
The library provides the `DefaultAzureCredentialOptionsExtension` class to set a proxy configuration for Azure Identity default credential options.

``` csharp
var proxyConfiguration = new ProxyConfiguration("http://proxy.example.com", 8080);
var credentialOptionsExtension = new DefaultAzureCredentialOptionsExtension
{
    ProxyConfiguration = proxyConfiguration
};

var httpClientHandler = credentialOptionsExtension.CreateHttpClientHandler();
```

## Proxy Configuration Validation
The ProxyConfiguration class validates the provided address and port to ensure they meet the required criteria.

``` csharp
var proxyConfiguration = new ProxyConfiguration("http://proxy.example.com", 8080);
```

## Loading Proxy Configuration
The ProxyUtils class provides methods to load proxy configurations from environment variables or application settings.

``` csharp
var proxyConfiguration = ProxyUtils.LoadProxyConfigurationFromEnvironment();
```

``` csharp
var proxyConfiguration = ProxyUtils.LoadProxyConfigurationFromAppSettings(configuration);
```

## Contributing
This repository is not accepting contributions at this time.

## License
MIT License