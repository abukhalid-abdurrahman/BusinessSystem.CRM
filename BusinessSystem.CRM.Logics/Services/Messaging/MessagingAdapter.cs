using System;
using System.Threading.Tasks;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.CRM.Logics.Services.Messaging
{
    public class MessagingAdapter
    {
        private readonly IMessagingService _messagingService;
        public MessagingAdapter(IMessagingService messagingService)
        {
            _messagingService = messagingService;
        }

        public async Task SendMessage(MessagingEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _messagingService.Send(entity);
        }

        public async Task EditMessage(MessagingEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _messagingService.Edit(entity);
        }

        public async Task ChangeMessageState(MessagingEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _messagingService.ChangeState(entity);
        }

        public async Task RemoveMessage(MessagingEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _messagingService.Remove(entity);
        }
    }
}