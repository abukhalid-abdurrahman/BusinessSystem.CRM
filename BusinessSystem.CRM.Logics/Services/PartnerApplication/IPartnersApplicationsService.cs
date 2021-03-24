using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Response;

namespace BusinessSystem.CRM.Logics.Services.PartnerApplication
{
    public interface IPartnersApplicationsService
    {
        public Task<List<PartnersApplicationsResponseModel>> GetInboxApplicationsAsync(int partnerId);
        public Task<PartnersApplicationsResponseModel> GetInboxApplicationAsync(int partnerId, int applicationId);
        public Task<PartnersApplicationsResponseModel> GetApplicationAsync(int applicationId);
        public Task<List<PartnersApplicationsResponseModel>> GetSentApplicationsAsync(int partnerId);
        public Task<List<PartnersApplicationsResponseModel>> GetRejectedApplicationsAsync(int partnerId);
        public Task<List<PartnersApplicationsResponseModel>> GetConfirmedApplicationsAsync(int partnerId);
        public Task RejectInboxApplication(int partnerId, int applicationId);
        public Task ConfirmInboxApplication(int partnerId, int applicationId);
        public Task<int> SendPartnerApplicationAsync(PartnersApplicationsEntityModel entityModel);
        public Task EditPartnerApplicationAsync(PartnersApplicationsEntityModel entityModel);
        public Task RemovePartnerApplicationAsync(int partnerId, int applicationId);
    }
}