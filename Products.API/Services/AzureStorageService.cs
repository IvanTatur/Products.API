using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Products.API.Interfaces;
using Products.API.Options;

namespace Products.API.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly AzureStorageOptions options;
        private readonly KeyVaultProvider keyVaultProvider;

        public AzureStorageService(IOptions<AzureStorageOptions> snapshot, KeyVaultProvider keyVaultProvider)
        {
            this.keyVaultProvider = keyVaultProvider;
            options = snapshot.Value;
        }

        public async Task UploadStreamAsync(Stream stream,
            string blobContainerName,
            string blobFilePath,
            CancellationToken cancellationToken)
        {
            var storageConnectionString = await keyVaultProvider.GetValueBySecretAsync(options.ConnectionString);
            var containerClient = new BlobContainerClient(storageConnectionString, blobContainerName);
            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
            await containerClient.UploadBlobAsync(blobFilePath, stream, cancellationToken);
        }
    }
}