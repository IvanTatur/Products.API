using Microsoft.AspNetCore.Http;

namespace Products.API
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
    }
}