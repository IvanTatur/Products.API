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

        /// <summary>
        ///     Initializes a new instance of the <see cref="AzureStorageService" /> class.
        /// </summary>
        /// <param name="snapshot">The snapshot.</param>
        public AzureStorageService(IOptions<AzureStorageOptions> snapshot)
        {
            options = snapshot.Value;
        }

        /// <inheritdoc />
        public async Task UploadStreamAsync(Stream stream,
            string blobContainerName,
            string blobFilePath,
            CancellationToken cancellationToken)
        {
            var containerClient = new BlobContainerClient(options.ConnectionString, blobContainerName);
            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
            await containerClient.UploadBlobAsync(blobFilePath, stream, cancellationToken);
        }
    }
}