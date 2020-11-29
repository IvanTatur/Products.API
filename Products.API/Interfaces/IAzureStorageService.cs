using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Products.API.Interfaces
{
    public interface IAzureStorageService
    {
        Task UploadStreamAsync(Stream stream, string blobContainerName, string blobFilePath,
            CancellationToken cancellationToken);
    }
}