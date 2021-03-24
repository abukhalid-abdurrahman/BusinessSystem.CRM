using System.Threading.Tasks;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.CRM.Logics.Services.Messaging
{
    public interface IMessagingService
    {
        Task Send(MessagingEntity messagingEntity);
        Task Edit(MessagingEntity messagingEntity);
        Task ChangeState(MessagingEntity messagingEntity);
        Task Remove(MessagingEntity messagingEntity);
    }
}