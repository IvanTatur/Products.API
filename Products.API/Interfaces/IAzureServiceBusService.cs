using System.Threading.Tasks;
using Products.API.Messages;

namespace Products.API.Interfaces
{
    public interface IAzureServiceBusService
    {
        Task SendAsync(MessageContract contract);
    }
}