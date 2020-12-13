using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Products.API.Services
{
    public class KeyVaultProvider
    {
        private readonly string keyVaultUri;
        private readonly ILogger<KeyVaultProvider> logger;

        public KeyVaultProvider(IConfiguration configuration, ILogger<KeyVaultProvider> logger)
        {
            this.logger = logger;
            var keyVaultName = configuration.GetValue<string>("KeyVaultName");
            keyVaultUri = "https://" + keyVaultName + ".vault.azure.net";
        }

        public async Task<string> GetValueBySecretAsync(string secretName)
        {
            var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

            try
            {
                KeyVaultSecret secret = await client.GetSecretAsync(secretName);
                return secret.Value;
            }
            catch (AuthenticationFailedException e)
            {
                var error = $"Authentication Failed. {e.Message}";
                logger.LogError(default, e, error);
                return null;
            }
            catch (Exception e)
            {
                logger.LogError(default, e, e.Message);
                return null;
            }
        }
    }
}