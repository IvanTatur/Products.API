using System.Collections.Generic;

namespace Products.API.Messages
{
    public class ProcessDocumentRequest : MessageContract
    {
        public ProcessDocumentRequest(string documentId, string fileName, IDictionary<string, string> metadata)
        {
            DocumentId = documentId;
            FileName = fileName;

            if (metadata == null) return;

            Metadata = new Dictionary<string, string>();
            foreach (var (key, value) in metadata) Metadata[key] = value;
        }

        public string DocumentId { get; }
        public string FileName { get; }

        public Dictionary<string, string> Metadata { get; set; }
    }
}