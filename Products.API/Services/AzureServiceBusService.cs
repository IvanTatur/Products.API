using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Products.API.Interfaces;
using Products.API.Messages;
using Products.API.Options;

namespace Products.API.Services
{
    public class AzureServiceBusService : IAzureServiceBusService
    {
        private readonly CloudQueueClient client;
        private readonly AzureQueueOptions options;
        private readonly ILogger<AzureServiceBusService> logger;

        public AzureServiceBusService(IOptions<AzureQueueOptions> options, ILogger<AzureServiceBusService> logger)
        {
            this.logger = logger;
            this.options = options.Value;
            client = CloudStorageAccount.Parse(this.options.ConnectionString).CreateCloudQueueClient();
        }

        public async Task SendAsync(MessageContract contract)
        {
            if (contract == null) throw new ArgumentNullException(nameof(contract));

            var queue = client.GetQueueReference(options.DocumentsQueueName);
            await queue.CreateIfNotExistsAsync();

            if (contract is ProcessDocumentRequest request)
            {
                var serializedPayload = JsonSerializer.Serialize(request);
                var message = new CloudQueueMessage(serializedPayload);
                await queue.AddMessageAsync(message);
            }
            else
            {
                throw new NotImplementedException();
            }

            logger.LogInformation("Message sent.");
        }
    }
}