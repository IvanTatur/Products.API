namespace Products.API.Options
{
    public class AzureStorageOptions
    {
        public const string SectionName = "FileStorage";
        public string ConnectionString { get; set; }
        public string DocumentsContainerName { get; set; }
    }
}