namespace AzureIdentityProxyEnhancement.Models
{
    public class ProxyConfiguration
    {
        private Uri? _proxyUri;

        public string Address
        {
            get => _proxyUri?.ToString() ?? string.Empty;
            set
            {
                if (!Uri.TryCreate(value, UriKind.Absolute, out Uri? result) || 
                    !(result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
                {
                    throw new ArgumentException("Proxy address must be a valid HTTP or HTTPS URL.", nameof(Address));
                }
                _proxyUri = result;
            }
        }

        public int Port
        {
            get => _proxyUri?.Port ?? 0;
            set
            {
                if (value <= 0 || value > 65535)
                {
                    throw new ArgumentOutOfRangeException(nameof(Port), "Port must be a positive number between 1 and 65535.");
                }
                if (_proxyUri != null)
                {
                    _proxyUri = new UriBuilder(_proxyUri.Scheme, _proxyUri.Host, value).Uri;
                }
            }
        }

        public ProxyConfiguration(string address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}