using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Products.API.Constants;
using Products.API.Interfaces;
using Products.API.Messages;
using Products.API.Options;

namespace Products.API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ILogger<DocumentService> logger;
        private readonly AzureStorageOptions options;
        private readonly IAzureServiceBusService serviceBusService;
        private readonly IAzureStorageService storageService;

        public DocumentService(ILogger<DocumentService> logger, IAzureStorageService storageService,
            IAzureServiceBusService serviceBusService, IOptions<AzureStorageOptions> options)
        {
            this.logger = logger;
            this.storageService = storageService;
            this.serviceBusService = serviceBusService;
            this.options = options.Value;
        }

        public async Task<object> UploadAsync(FileUploadModel fileUpload, CancellationToken cancellationToken)
        {
            var fileName = fileUpload.File.FileName;
            var documentId = Guid.NewGuid().ToString();
            var path = string.Format(ApiConstants.PathFormat, documentId, fileName);


            logger.LogInformation($"File upload started to: {path}.");

            await using var fileStream = fileUpload.File.OpenReadStream();
            await storageService.UploadStreamAsync(fileStream, options.DocumentsContainerName, path, cancellationToken);

            logger.LogInformation("File upload completed.");

            var metadata = new Dictionary<string, string> {{"FileName", fileName}};
            var documentRequest = new ProcessDocumentRequest(documentId, fileName, metadata);

            await serviceBusService.SendAsync(documentRequest);

            return path;
        }
    }
}