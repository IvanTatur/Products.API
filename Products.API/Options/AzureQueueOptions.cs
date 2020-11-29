namespace Products.API.Options
{
    public class AzureQueueOptions
    {
        public const string SectionName = "MessageBus";
        public string ConnectionString { get; set; }
        public string DocumentsQueueName { get; set; }
    }
}