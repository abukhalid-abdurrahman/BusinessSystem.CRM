using System;
using System.Threading.Tasks;
using BusinessSystem.CRM.Logics.Services.PartnerApplication;
using BusinessSystem.Database.Models.BusinessObjects;

namespace BusinessSystem.CRM.Logics.Services.Messaging
{
    public class ApplicationMessaging : IMessagingService
    {
        private readonly IPartnersApplicationsService _applicationsService;
        public ApplicationMessaging(IPartnersApplicationsService applicationsService)
        {
            _applicationsService = applicationsService;
        }
        public async Task Send(MessagingEntity messagingEntity)
        {
            if(messagingEntity == null)
                throw new ArgumentNullException(nameof(messagingEntity));
            messagingEntity.MessageId = await _applicationsService.SendPartnerApplicationAsync(new PartnersApplicationsEntityModel()
            {
                Id = messagingEntity.MessageId,
                SenderId = messagingEntity.SenderId,
                RecipientId = messagingEntity.RecipientId,
                ApplicationText = messagingEntity.Message,
                Confirmed = messagingEntity.State
            });
        }

        public async Task Edit(MessagingEntity messagingEntity)
        {
            if(messagingEntity == null)
                throw new ArgumentNullException(nameof(messagingEntity));
            await _applicationsService.EditPartnerApplicationAsync(new PartnersApplicationsEntityModel()
            {
                Id = messagingEntity.MessageId,
                SenderId = messagingEntity.SenderId,
                RecipientId = messagingEntity.RecipientId,
                ApplicationText = messagingEntity.Message,
                Confirmed = messagingEntity.State
            });
        }

        public async Task ChangeState(MessagingEntity messagingEntity)
        {
            if(messagingEntity == null)
                throw new ArgumentNullException(nameof(messagingEntity));
            if (!messagingEntity.State)
                await _applicationsService.RejectInboxApplication(messagingEntity.SenderId, messagingEntity.MessageId);
            else
                await _applicationsService.ConfirmInboxApplication(messagingEntity.SenderId, messagingEntity.MessageId);
        }

        public async Task Remove(MessagingEntity messagingEntity)
        {
            if(messagingEntity == null)
                throw new ArgumentNullException(nameof(messagingEntity));

            await _applicationsService.RemovePartnerApplicationAsync(messagingEntity.SenderId, messagingEntity.MessageId);
        }
    }
}