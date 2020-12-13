using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.API.Constants;
using Products.API.Interfaces;
using Products.API.Options;
using Products.API.Services;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService documentService;
        private readonly ILogger<DocumentsController> logger;
        private readonly ApiOptions options;

        public DocumentsController(IOptions<ApiOptions> options, IDocumentService documentService,
            ILogger<DocumentsController> logger)
        {
            this.documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
            this.logger = logger;
            this.options = options.Value;
        }

        [HttpPost(Name = nameof(UploadDocumentsAsync))]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadDocumentsAsync([FromForm] FileUploadModel fileUploadModel,
            CancellationToken cancellationToken)
        {
            logger.LogInformation($"UploadDocuments started {fileUploadModel.File.FileName}.");

            var maxFileLength = options.MaxFileLength;

            if (fileUploadModel.File.Length > maxFileLength)
                return BadRequest(string.Format(ErrorMessages.InvalidFileSizeFormat, maxFileLength));

            var extension = Path.GetExtension(fileUploadModel.File.FileName);

            if (extension == null || !ApiConstants.AllowedExtensionsRegEx.IsMatch(extension))
                return BadRequest(ErrorMessages.InvalidExtension);


            var result = await documentService.UploadAsync(fileUploadModel, cancellationToken);


            logger.LogInformation($"Upload completed {fileUploadModel.File.FileName}.");

            return new JsonResult(result)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}