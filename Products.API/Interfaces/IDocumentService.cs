using System.Threading;
using System.Threading.Tasks;

namespace Products.API.Interfaces
{
    public interface IDocumentService
    {
        Task<object> UploadAsync(FileUploadModel formFile, CancellationToken cancellationToken);
    }
}